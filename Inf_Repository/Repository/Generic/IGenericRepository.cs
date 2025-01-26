namespace Inf_Repository.Repository.Generic;

public interface IGenericRepository
<TEntity>where TEntity : class
{
    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> GetAllWithTracking();
    Task<TEntity> GetById(int id);

    Task Create(TEntity entity);

    Task Delete(int id);
}