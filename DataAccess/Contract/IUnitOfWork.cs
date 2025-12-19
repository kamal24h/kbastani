using System.Threading.Tasks;

namespace DataAccess.Contract;

public interface IUnitOfWork
{
    #region Repository Announcements

    IBlogPostRepository BlogPostRepository { get; }
    //IAccEventDetailRepository AccEventDetailRepository { get; }
    //ISysUserRepository SysUserRepository { get; }

    #endregion

    #region Methods

    int SaveChanges();

    Task<int> SaveChangesAsync();

    void RejectChanges();

    #endregion 
}


