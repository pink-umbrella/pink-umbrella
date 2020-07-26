using System.Threading.Tasks;
using PinkUmbrella.Models.Search;
using PinkUmbrella.Services.Search;
using Nest;
using System.Collections.Generic;
using Tides.Actors;
using Tides.Services;

namespace PinkUmbrella.Services.Elastic.Search
{
    public class ElasticSearchProfilesService : BaseSearchElasticService<ActorObject>, ISearchableService
    {
        private readonly IPublicProfileService _profiles;

        public ElasticSearchProfilesService(IPublicProfileService profiles, IHazActivityStreamPipe pipe): base(pipe)
        {
            _profiles = profiles;
        }

        public SearchResultType ResultType => SearchResultType.Profile;

        public SearchSource Source => SearchSource.Elastic;

        public string ControllerName => "Profile";

        public async Task<SearchResultsModel> Search(SearchRequestModel request)
        {
            var elastic = GetClient();
            var musts = new List<QueryContainer>();

            if (!string.IsNullOrWhiteSpace(request.text))
            {
                musts.Add(new BoolQuery
                {
                    Should = new List<QueryContainer>
                    {
                        new MatchQuery() { Field = "name", Query = request.text, Boost = .8 },
                        new MatchQuery() { Field = "handle", Query = request.text, Boost = 1 },
                        new MatchQuery() { Field = "summary", Query = request.text, Boost = 0.5 },
                    }
                });
            }

            AddTagSearch(request, musts);
            return await DoSearch(request, elastic, new BoolQuery()
            {
                Must = musts,
            }, null, ResultType);
        }
    }
}