using Domain;

namespace DataAccess.Contract
{
    public interface IBlogPostRepository : ICrudRepository<BlogPost>
    {
        Task<BlogPost> GetById(long id);
        Task<BlogPost> GetByGuid(Guid id);
        Task<List<BlogPost>> GetListAsync();
    }
}
