using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Toolkit.UoW.Abstractions;

namespace Toolkit.UoW.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DbContext Context;
        private DbSet<T> _entities;

        public RepositoryBase(DbContext context)
        {
            Context = context;
            _entities = context.Set<T>();
        }

        public DbContext SetContext(DbContext context)
        {
            Context = context;
            return Context;
        }

        public T Add(T model)
        {
            return _entities.Add(model).Entity;
        }


        public IEnumerable<T> AddRange(IEnumerable<T> model)
        {
            _entities.AddRange(model);

            return model;
        }

        public T Find(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> includes = null)
        {
            var query = includes != null ? includes(_entities) : _entities;
            return query.AsNoTracking().SingleOrDefault(predicate);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IQueryable<T>> includes = null)
        {
            var query = _entities.AsNoTracking();

            if (includes != null)
            {
                query = includes(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.ToList();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, int index, int length, out int total, Func<IQueryable<T>, IQueryable<T>> includes)
        {
            var query = includes(_entities).AsNoTracking();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            total = query.Count();

            return query.Skip(index * length).Take(length);
        }

        public IEnumerable<T> FindAll(out int total, Expression<Func<T, bool>> predicate = null, int? index = null, int? length = null, Expression<Func<T, object>> orderBy = null, bool orderByAscending = true, Func<IQueryable<T>, IQueryable<T>> includes = null)
        {
            var query = _entities.AsNoTracking();

            if (includes != null)
            {
                query = includes(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            // must take current total before paging
            total = query.Count();

            if (orderBy != null)
            {
                if (orderByAscending)
                {
                    query = query.OrderBy(orderBy);
                }
                else
                {
                    query = query.OrderByDescending(orderBy);
                }
            }

            if (index.HasValue)
            {
                query = query.Skip(index.Value * length.Value);
            }

            if (length.HasValue)
            {
                query = query.Take(length.Value);
            }

            return query;
        }

        public void Remove(Expression<Func<T, bool>> predicate)
        {
            var items = FindAll(predicate);
            _entities.RemoveRange(items);
        }

        public T Update(T model)
        {
            var entity = _entities.Update(model).Entity;
            return entity;
        }

        public void UpdateRange(IEnumerable<T> model)
        {
            _entities.UpdateRange(model);
        }
    }
}
