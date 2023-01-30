namespace Lazy.DB
{
    public interface IRepository<TEntity, TKey> 
    {
        TEntity Create(TEntity entity);
        TEntity Read(TKey id);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
    }

    public abstract class EntityBaseIntKey 
    {
        public int Id { get; set; }
    }
}
