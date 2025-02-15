using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
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
            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    entity = entity.Include(include);
                }
            }

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
            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    entity = entity.Include(include);
                }
            }

            if (condition != null)
            {
                entity = entity.Where(condition);
            }

            return await PagedList<T>.ToPagedListAsync(
                entity,
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
            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    entity = entity.Include(include);
                }
            }

            if (condition != null)
            {
                foreach (var expression in condition)
                {
                    entity = entity.Where(expression);
                }
            }

            return await PagedList<T>.ToPagedListAsync(
                entity.AsSingleQuery(),
                queryStringParameters.PageNumber,
                queryStringParameters.PageSize,
                queryStringParameters.DesOrder);
        }

        public async Task<T> GetByConditionsAsync(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes)
        {
            var entity = _dbContext.Set<T>().IgnoreAutoIncludes();
            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    entity = entity.Include(include);
                }
            }

            return await entity.SingleOrDefaultAsync(condition);
        }

        public async Task<T> GetByIdAsync(int id)
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
            var entity1 = await GetByIdAsync(id);

            return _dbContext.Set<T>().Remove(entity1).Entity;
        }

        public async Task<T> Delete(T entity)
        {
            return _dbContext.Set<T>().Remove(entity).Entity;
        }

        public async Task<T> Update(int Id, T updatedEntity)
        {
            return _dbContext.Set<T>().Update(await GetByIdAsync(Id)).Entity;
        }

        public async Task<T> Update(T updatedEntity)
        {
            return _dbContext.Set<T>().Update(updatedEntity).Entity;
        }

        public async Task<IQueryable<T>> GetAsQueryable()
        {
            return _dbContext.Set<T>().AsQueryable<T>();
        }
    }
}
