using System.Collections.Generic;

namespace Tides.Models
{
    public class PaginatedModel<T>
    {
        public int Total { get; set; }
        public List<T> Items { get; set; }
        public PaginationModel Pagination { get; set; }
    }
}