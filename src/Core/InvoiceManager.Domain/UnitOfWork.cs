using System.Data;
using Toolkit.UoW.Abstractions;
using Toolkit.UoW.Repository;

namespace InvoiceManager.Domain
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InvoiceDbContext _databaseContext;
        // private IRepositoryBase<Invoice> _invoiceRepository;

        public UnitOfWork(InvoiceDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IRepositoryBase<T> GetRepository<T>() where T : class
        {
            return new RepositoryBase<T>(_databaseContext);
        }

        public void SetLazyLoading(bool enabled)
        {
            _databaseContext.ChangeTracker.LazyLoadingEnabled = enabled;
        }

        public bool SaveChanges()
        {
            return _databaseContext.SaveChanges() > 0;
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            _databaseContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _databaseContext.Database.CommitTransaction();
        }

        public void Rollback()
        {
            _databaseContext.Database.RollbackTransaction();
        }
    }
}
