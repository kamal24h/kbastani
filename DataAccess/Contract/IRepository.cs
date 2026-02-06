using System.Linq.Expressions;
using Domain;

namespace DataAccess.Contract;

public interface IRepository<T> where T: class
{
    IQueryable<T> Where(Expression<Func<T, bool>> predicate);

}

//Base Repository for CRUD Operations
public interface ICrudRepository<T> : IRepository<T> where T : class
{
    //EntityToContext
    Task<List<T>> GetAll();
    Task<T> GetById(long id);
    Task<T> GetByGuid(Guid guid);
    Task<T> AddAsync(T entity);
    T Update (T entity);
    //T Delete (T entity);
    Task<bool> DeleteAsync (long id);
}

