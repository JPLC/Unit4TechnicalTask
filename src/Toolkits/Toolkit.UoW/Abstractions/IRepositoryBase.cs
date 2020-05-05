using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Toolkit.UoW.Repository;

namespace Toolkit.UoW.Abstractions
{
    public interface IRepositoryBase<T> : IRepositoryInjection where T : class
    {
        T Add(T model);
        IEnumerable<T> AddRange(IEnumerable<T> model);

        T Find(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> includes = null);

        IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IQueryable<T>> includes = null);

        IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, int index, int length, out int total,
            Func<IQueryable<T>, IQueryable<T>> includes = null);

        IEnumerable<T> FindAll(out int total, Expression<Func<T, bool>> predicate, int? index,
            int? length, Expression<Func<T, object>> orderBy, bool orderByAscending = true,
            Func<IQueryable<T>, IQueryable<T>> includes = null);

        void Remove(Expression<Func<T, bool>> predicate);

        T Update(T model);

        void UpdateRange(IEnumerable<T> model);
    }
}
