namespace Lazy.Data
{
    public class PagedResult<T>
    {

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public List<T> Results { get; set; } = new();
        public int Count { get; set; }
    }
}
