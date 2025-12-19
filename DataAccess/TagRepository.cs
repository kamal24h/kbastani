using System.Linq.Expressions;
using DataAccess.Contract;
using Domain;

namespace DataAccess
{

    public class TagRepository : ITagRepository
    {
        public Tag Add(Tag entity)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> AddAsync(Tag entity)
        {
            throw new NotImplementedException();
        }

        public Tag Delete(Tag entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBy(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Tag>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Tag> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Tag> GetByGuid(Guid guid)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Tag Update(Tag entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tag> Where(Expression<Func<Tag, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
