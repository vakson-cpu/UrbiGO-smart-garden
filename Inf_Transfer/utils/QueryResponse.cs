using Inf_Data.Entities;

namespace Inf_Transfer.utils
{
    public class QueryResponse<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
    }

}