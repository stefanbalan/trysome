namespace Lazy.Shared
{
    public class PagedResult<T>
    {

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public string SearchString { get; set; }
        public List<T> Results { get; set; }
    }
}
