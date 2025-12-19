using DataAccess.Contract;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class UnitOfWork(AppDbContext _dbContext) : IDisposable, IUnitOfWork
{       
    #region Repositories

    private IBlogPostRepository? _blogPostRepository;
    public IBlogPostRepository BlogPostRepository =>
        _blogPostRepository = new BlogPostRepository(_dbContext);
    
    #endregion

    #region Interface Implementation

    public int SaveChanges()
    {
        return _dbContext.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        var result = await _dbContext.SaveChangesAsync();
        return result;
        //try
        //{
        //    using var transaction = _dbContext.Database.BeginTransaction();
        //    var result = await _dbContext.SaveChangesAsync();
        //    transaction.Commit();
        //    return result;
        //}
        //catch (Exception ex)
        //{

        //    return 0;
        //}
    }

    public void RejectChanges()
    {
        foreach (var entity in _dbContext.ChangeTracker.Entries())
        {
            switch (entity.State)
            {
                case EntityState.Modified:
                case EntityState.Deleted:
                    entity.State = EntityState.Modified; //revert changes made to deleted entity
                    entity.State = EntityState.Unchanged;
                    break;
                case EntityState.Added:
                    entity.State = EntityState.Detached;
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    #endregion

    #region Dispose & Garbage Collection

    private bool _disposing = false;

    protected virtual void Dispose(bool disposing)
    {
        if (_disposing) return;
        if (disposing)
            _dbContext.Dispose();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion
}
