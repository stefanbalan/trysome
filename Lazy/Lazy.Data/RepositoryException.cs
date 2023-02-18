namespace Lazy.Data
{
    public class RepositoryException : Exception
    {
        public RepositoryException(Type entityType,  string? message, Exception? innerException) : base(message, innerException)
        {
            
        }
    }
}
