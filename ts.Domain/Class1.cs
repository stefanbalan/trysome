namespace ts.Domain
{
    public interface IReposirory<TEntity, TKey> 
    {
        TEntity Create(TEntity entity);
        TEntity Read(TKey id);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
