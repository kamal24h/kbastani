using DataAccess.Contract;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess;

public class ProjectRepository(AppDbContext _dbContext) : IProjectRepository
{
    
    #region Read

    public async Task<List<Project>> GetAll()
    {
        var result = await _dbContext.Projects.Include(p => p.Techs).ToListAsync();
        return result;
    }

    public async Task<Project> GetById(long id)
    {
        var result = await _dbContext.Projects.Where(a => a.ProjectId == id).Include(p => p.Techs).SingleAsync();
        return result;
    }

    public async Task<Project> GetByGuid(Guid id)
    {
        var result = await _dbContext.Projects.Where(a => a.ProjectGuid == id).Include(p => p.Techs).SingleAsync();
        return result;
    }
    
    public async Task<Project> AddAsync(Project entity)
    {
        await _dbContext.Projects.AddAsync(entity);
        return entity;
    }

    public Project Update(Project entity)
    {
        _dbContext.Projects.Update(entity);
        return entity;
    }

    public Project Delete(Project entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Project> Where(Expression<Func<Project, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Internal

    #endregion
}