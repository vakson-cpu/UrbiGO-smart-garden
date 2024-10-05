namespace Inf_Repository.Repository.Generic;

public interface IGenericRepository
<TEntity>where TEntity : class
{
    IQueryable<TEntity> GetAll();

    Task<TEntity> GetById(int id);

    Task Create(TEntity entity);

    Task Delete(int id);
}