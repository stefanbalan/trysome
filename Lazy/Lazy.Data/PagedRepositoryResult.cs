namespace Lazy.Data
{
    public record PagedRepositoryResult<T>
    {

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public List<T> Results { get; set; } = new();
        public int Count { get; set; }
    }
}
