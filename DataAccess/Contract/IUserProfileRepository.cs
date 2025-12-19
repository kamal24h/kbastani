using Domain;

namespace DataAccess.Contract;

public interface IUserProfileRepository : ICrudRepository<UserProfile>
{
    Task<List<UserProfile>> Get();
    Task<UserProfile> GetById(long id);
    Task<UserProfile> GetByGuid(Guid guid);
}