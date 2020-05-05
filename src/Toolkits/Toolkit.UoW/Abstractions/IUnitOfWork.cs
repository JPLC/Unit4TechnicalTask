using System.Data;

namespace Toolkit.UoW.Abstractions
{
    public interface IUnitOfWork
    {
        IRepositoryBase<T> GetRepository<T>() where T : class;
        void SetLazyLoading(bool enabled);
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        bool SaveChanges();
        void Commit();
        void Rollback();
    }
}
