using DataAccess.Contract;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess;

public class UserProfileRepository(AppDbContext _dbContext) : IUserProfileRepository
{
    
    #region Read

    public async Task<List<UserProfile>> Get()
    {
        var result = await _dbContext.UserProfiles.ToListAsync();
        return result;
    }

    public async Task<UserProfile> GetById(long id)
    {
        var result = await _dbContext.UserProfiles.Where(a => a.UserProfileId == id).SingleAsync();
        return result;
    }

    public async Task<UserProfile> GetByGuid(Guid id)
    {
        var result = await _dbContext.UserProfiles.Where(a => a.UserProfileGuid == id).SingleAsync();
        return result;
    }

    public IQueryable<UserProfile> Where(Expression<Func<UserProfile, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Create

    public UserProfile Add(UserProfile entity)
    {
        _dbContext.UserProfiles.Add(entity);
        return entity;
    }

    public async Task<UserProfile> AddAsync(UserProfile entity)
    {
        await _dbContext.UserProfiles.AddAsync(entity);
        return entity;
    }

    #endregion

    #region Update

    public UserProfile Update(UserProfile entity)
    {
        _dbContext.UserProfiles.Update(entity);
        return entity;
    }

    #endregion

    #region Delete

    public UserProfile Delete(UserProfile entity)
    {
        throw new NotImplementedException();
    }

    public bool DeleteBy(int id)
    {
        throw new NotImplementedException();
    }

    #endregion

    

    

    
    
    
    #region Internal

    #endregion
}