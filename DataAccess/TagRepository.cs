using System;
using System.Linq.Expressions;
using DataAccess.Contract;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{

    public class TagRepository(AppDbContext ctx) : ITagRepository
    {        
        public async Task<Tag> AddAsync(Tag entity)
        {
            await ctx.AddAsync(entity);
            return entity;
        }
        
        public async Task<List<Tag>> GetAll()
        {
            var result = await ctx.Tags.ToListAsync();
            return result;
        }

        public async Task<List<Tag>> GetAsync()
        {
            var result = await ctx.Tags.ToListAsync();
            return result;
        }

        public async Task<Tag> GetByGuid(Guid guid)
        {
            var result = await ctx.Tags.Where(a => a.TagGuid == guid).SingleAsync();
            return result;
        }

        public async Task<Tag> GetById(long id)
        {
            var result = await ctx.Tags.Where(a => a.TagId == id).SingleAsync();
            return result;
        }

        public Tag Update(Tag entity)
        {
            ctx.Update(entity);
            return entity;
        }

        public IQueryable<Tag> Where(Expression<Func<Tag, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await GetById(id);
            if (entity == null)
                return false;
            ctx.Remove(entity);
            return true;
        }        
    }
}
