using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PinkUmbrella.Models;
using PinkUmbrella.Repositories;

namespace PinkUmbrella.Services.Sql.Search
{
    public class SearchInventoryService : ISearchableService
    {
        private readonly SimpleDbContext _dbContext;

        public SearchInventoryService(SimpleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public SearchResultType ResultType => SearchResultType.Inventory;

        public string ControllerName => "Inventory";

        public async Task<SearchResultsModel> Search(string text, SearchResultOrder order, PaginationModel pagination)
        {
            var query = _dbContext.Inventories.Where(p => p.DisplayName.Contains(text) || p.Description.Contains(text));
            
            switch (order) {
                // case SearchResultOrder.Top:
                // case SearchResultOrder.Hot:
                // query = query.OrderBy(q => q.LikeCount);
                // break;
                default:
                case SearchResultOrder.Latest:
                query = query.OrderBy(q => q.WhenCreated);
                break;
            }

            var totalCount = query.Count();
            var results = await query.Skip(pagination.start).Take(pagination.count).ToListAsync();
            return new SearchResultsModel() {
                Results = results.Select(p => new SearchResultModel() {
                    Type = ResultType,
                    Value = p,
                }).ToList(),
                TotalResults = totalCount
            };
        }
    }
}