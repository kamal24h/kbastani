using Domain;

namespace DataAccess.Contract
{
    public interface ITagRepository : ICrudRepository<Tag>
    {
        Task<List<Tag>> Get();
        Task<Tag> GetById(long id);
        Task<Tag> GetByGuid(Guid guid);
    }
}
