using DuplicateFileFind;

namespace DFF;

public class IndexDatabase
{
    public IndexDatabase(DffContext context)
    {
        context.Database.EnsureCreated();
    }

    public bool HasFile(Item file)
    {
        throw new NotImplementedException();
    }

    public void AddFile(Item file)
    {
        throw new NotImplementedException();
    }
}