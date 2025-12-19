using Domain;

namespace DataAccess.Contract;

public interface IBlogCategoryRepository : ICrudRepository<BlogCategory>
{
    Task<List<BlogCategory>> Get();
    Task<BlogCategory> GetById(long id);
    Task<BlogCategory> GetByGuid(Guid guid);
}