using Microsoft.EntityFrameworkCore;

namespace Toolkit.UoW.Repository
{
    public interface IRepositoryInjection
    {
        DbContext SetContext(DbContext context);

    }
}