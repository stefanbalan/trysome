namespace Lazy.Model
{
    public record PagedModelResult<T>
    {

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public List<T> Results { get; set; } = new();
        public int Count { get; set; }
    }
}
