using Domain;

namespace DataAccess.Contract
{
    public interface IProjectRepository : ICrudRepository<Project>
    {
        Task<Project> GetById(long id);
        Task<Project> GetByGuid(Guid id);
        Task<List<Project>> GetListAsync();
    }
}
