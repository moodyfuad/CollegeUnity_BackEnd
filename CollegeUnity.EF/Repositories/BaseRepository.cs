using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using EFC = Microsoft.EntityFrameworkCore.EF;

namespace CollegeUnity.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {
        private readonly CollegeUnityDbContext _dbContext;

        public BaseRepository(CollegeUnityDbContext context)
        {
            _dbContext = context;
        }

        public async Task<PagedList<T>> GetRangeAsync(
            QueryStringParameters queryStringParameters,
            params Expression<Func<T, object>>[]? includes)
        {
            var entity = _dbContext.Set<T>().IgnoreAutoIncludes();

            _Include(ref entity, includes);

            _OrderBy(ref entity, queryStringParameters);
            return await PagedList<T>.ToPagedListAsync(
                entity,
                queryStringParameters.PageNumber,
                queryStringParameters.PageSize,
                queryStringParameters.DesOrder);
        }

        public async Task<PagedList<T>> GetRangeByConditionsAsync(
            Expression<Func<T, bool>> condition,
            QueryStringParameters queryStringParameters,
            params Expression<Func<T, object>>[] includes)
        {
            var entity = _dbContext.Set<T>().IgnoreAutoIncludes();

            _Include(ref entity, includes);

            if (condition != null && entity.Any())
            {
                entity = entity.Where(condition);
            }

            _OrderBy(ref entity, queryStringParameters);


            return await PagedList<T>.ToPagedListAsync(
                entity??= new List<T>().AsQueryable(),
                queryStringParameters.PageNumber,
                queryStringParameters.PageSize,
                queryStringParameters.DesOrder);
        }

        public async Task<PagedList<T>> GetRangeByConditionsAsync(
            Expression<Func<T, bool>>[]? condition,
            QueryStringParameters queryStringParameters,
            params Expression<Func<T, object>>[] includes)
        {
            var entity = _dbContext.Set<T>().IgnoreAutoIncludes();

            _Include(ref entity, includes);

            if (condition != null)
            {
                foreach (var expression in condition)
                {
                    if (entity != null && entity.Any())
                    {
                        entity = entity.Where(expression);
                    }
                }
            }

            _OrderBy(ref entity, queryStringParameters);

            return await PagedList<T>.ToPagedListAsync(
                entity?.AsSingleQuery() ?? new List<T>().AsQueryable(),
                queryStringParameters.PageNumber,
                queryStringParameters.PageSize,
                queryStringParameters.DesOrder);
        }

        public async Task<T?> GetByConditionsAsync(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes)
        {
            var entity = _dbContext.Set<T>().IgnoreAutoIncludes();

            _Include(ref entity, includes);

            var result = await entity.FirstOrDefaultAsync(condition);

            return result;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
           return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> CreateAsync(T entity)
        {
               var entity1 = await _dbContext.Set<T>().AddAsync(entity);
            return entity1.Entity;
        }

        public async Task<T> Delete(int id)
        {
            var entity = await GetByIdAsync(id);

            return _dbContext.Set<T>().Remove(entity).Entity;
        }

        public async Task<T> Delete(T entity)
        {
            return _dbContext.Set<T>().Remove(entity).Entity;
        }

        public async Task<T> Update(int Id, T updatedEntity)
        {
            return _dbContext.Set<T>().Update(await GetByIdAsync(Id)).Entity;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            return entity != null;
        }

        public async Task<T> Update(T updatedEntity)
        {
            return _dbContext.Set<T>().Update(updatedEntity).Entity;
        }

        public async Task<IQueryable<T>> GetAsQueryable()
        {
            return _dbContext.Set<T>().AsQueryable<T>();
        }

        protected static void _OrderBy(ref IQueryable<T>? entity, QueryStringParameters queryString)
        {
            if (entity == null)
            {
                return;
            }
            else if (!string.IsNullOrEmpty(queryString.ThenBy))
            {
                _OrderByWithThenBy(ref entity, queryString);
            }
            else if (!string.IsNullOrEmpty(queryString.OrderBy))
            {
                try
                {
                    var property = typeof(T).GetProperty(queryString.OrderBy, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                    if (property != null)
                    {
                        entity = !queryString.DesOrder
                            ? entity.OrderBy(x => EFC.Property<object>(x, property.Name))
                            : entity.OrderByDescending(x => EFC.Property<object>(x, property.Name));
                    }
                }
                catch
                {
                }
            }
        }

        private static void _OrderByWithThenBy(ref IQueryable<T>? entity, QueryStringParameters queryString)
        {
            if (entity != null && !string.IsNullOrEmpty(queryString.OrderBy))
            {
                try
                {
                    var propertyOrder = typeof(T).GetProperty(queryString.OrderBy, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                    var propertyThen = typeof(T).GetProperty(queryString.ThenBy!, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                    if (propertyOrder != null && propertyThen != null)
                    {
                        entity = !queryString.DesOrder
                            ? entity.OrderBy(x => EFC.Property<object>(x, propertyOrder.Name))
                            .ThenBy(x => EFC.Property<object>(x, propertyThen.Name))
                            : entity.OrderByDescending(x => EFC.Property<object>(x, propertyOrder.Name))
                            .ThenBy(x => EFC.Property<object>(x, propertyThen.Name));
                    }
                }
                catch
                {
                }
            }
        }

        protected static void _Include(ref IQueryable<T> entity, params Expression<Func<T, object>>[]? includes)
        {
            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    if (entity != null && entity.Any())
                    {
                        entity = entity.Include(include);
                    }
                }
            }
        }
    }
}
