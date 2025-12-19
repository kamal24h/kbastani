using DataAccess.Contract;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess;

public class BlogPostRepository(AppDbContext _dbContext) : IBlogPostRepository
{
    
    #region Read

    public async Task<List<BlogPost>> GetListAsync()
    {
        var result = await _dbContext.BlogPosts.ToListAsync();
        return result;
    }

    public async Task<BlogPost> GetById(long id)
    {
        var result = await _dbContext.BlogPosts.Where(a => a.BlogPostId == id).SingleAsync();
        return result;
    }

    public async Task<BlogPost> GetByGuid(Guid id)
    {
        var result = await _dbContext.BlogPosts.Where(a => a.BlogPostGuid == id).SingleAsync();
        return result;
    }
    
    public BlogPost Add(BlogPost entity)
    {
        _dbContext.BlogPosts.Add(entity);
        return entity;
    }

    public async Task<BlogPost> AddAsync(BlogPost entity)
    {
        await _dbContext.BlogPosts.AddAsync(entity);
        return entity;
    }

    public BlogPost Update(BlogPost entity)
    {
        _dbContext.BlogPosts.Update(entity);
        return entity;
    }

    public BlogPost Delete(BlogPost entity)
    {
        throw new NotImplementedException();
    }

    public bool DeleteBy(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<BlogPost> Where(Expression<Func<BlogPost, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    

    #endregion


    #region Internal

    #endregion
}