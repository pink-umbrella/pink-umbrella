using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estuary.Services;
using Nest;
using PinkUmbrella.Models.Search;
using Estuary.Core;
using static Estuary.Activities.Common;

namespace PinkUmbrella.Services.Elastic
{
    public abstract class BaseSearchElasticService<T>: BaseElasticService where T: BaseObject
    {
        private IHazActivityStreamPipe _pipeline;

        public BaseSearchElasticService(IHazActivityStreamPipe pipeline)
        {
            _pipeline = pipeline;
        }

        protected async Task<SearchResultsModel> HandleResponse(SearchRequestModel request, Nest.ISearchResponse<T> response, SearchResultType resultType)
        {
            if (response.IsValid && response.Documents.Count > 0)
            {
                var sources = response.HitsMetadata.Hits.Select(h => h.Source);
                var highlights = response.HitsMetadata.Hits.Select(h => h.Highlight);
                var results = new List<BaseObject>();
                var pipe = _pipeline.GetPipe();
                foreach (var r in sources)
                {
                    r.ViewerId = request.viewerId;
                    var final = await pipe.Pipe(new ActivityDeliveryContext { item = new Announce { obj = r }, IsReading = true });
                    if (final != null)
                    {
                        results.Add(final);
                    }
                }
                return new SearchResultsModel()
                {
                    Results = results.Skip(request.pagination.start).Take(request.pagination.count).Select(p => new SearchResultModel()
                    {
                        Type = resultType,
                        Value = p,
                    }).ToList(),
                    TotalResults = results.Count()
                };
            }
            return new SearchResultsModel();
        }

        protected void AddTagSearch(SearchRequestModel request, List<QueryContainer> musts)
        {
            if (request.tags != null && request.tags.Length > 0)
            {
                musts.Add(new BoolQuery
                {
                    Should = request.tags.Select(t => (QueryContainer) new MatchQuery() { Field = "tags", Query = t }).ToList()
                });
            }
        }

        protected async Task<SearchResultsModel> DoSearch(SearchRequestModel request, ElasticClient elastic, QueryContainer query, QueryContainer filter, SearchResultType resultType)
        {
            var searchRequest = new SearchRequest<T>
            {
                Query = query,
                PostFilter = filter,
                From = request.pagination.start,
                Size = request.pagination.count,
            };
            var response = await elastic.SearchAsync<T>(searchRequest);
            return await HandleResponse(request, response, resultType);
        }
    }
}