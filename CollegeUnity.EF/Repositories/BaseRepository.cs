using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Helpers;
using CollegeUnity.EF.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
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
            bool trackChanges = false,
            params Expression<Func<T, object>>[] includes)
        {
            var entity = _dbContext.Set<T>().IgnoreAutoIncludes();

            entity = entity.IncludeOneOrMany(includes);

            entity = entity.OrderBy(queryStringParameters);

            entity = entity.TrackChanges(trackChanges);

            return await entity.AsPagedListAsync(queryStringParameters);
        }

        public async Task<PagedList<T>> GetRangeByConditionsAsync(
            Expression<Func<T, bool>> condition,
            QueryStringParameters queryStringParameters,
            bool trackChanges = false,
            params Expression<Func<T, object>>[] includes)
        {
            var entity = _dbContext.Set<T>().IgnoreAutoIncludes();

            entity = entity.IncludeOneOrMany(includes);

            entity = entity.Condition(condition);

            entity = entity.OrderBy(queryStringParameters);

            entity = entity.TrackChanges(trackChanges);


            return await entity.AsPagedListAsync(queryStringParameters);
        }

        public async Task<PagedList<T>> GetRangeByConditionsAsyncDes(
            Expression<Func<T, bool>> condition,
            QueryStringParameters queryStringParameters,
            bool trackChanges = false,
            params Expression<Func<T, object>>[] includes)
        {
            var entity = _dbContext.Set<T>().IgnoreAutoIncludes();

            entity = entity.IncludeOneOrMany(includes);

            entity = entity.Condition(condition);

            entity = entity.OrderDescending();

            entity = entity.TrackChanges(trackChanges);


            return await entity.AsPagedListAsync(queryStringParameters);
        }

        public async Task<PagedList<T>> GetRangeByConditionsAsync(
            Expression<Func<T, bool>>[] condition,
            QueryStringParameters queryStringParameters,
            bool trackChanges = false,
            params Expression<Func<T, object>>[] includes)
        {
            var entity = _dbContext.Set<T>().IgnoreAutoIncludes();

            entity = entity.IncludeOneOrMany(includes);

            entity = entity.Condition(condition);

            entity = entity.OrderBy(queryStringParameters);

            entity = entity.TrackChanges(trackChanges);

            return await entity.AsPagedListAsync(queryStringParameters);
        }

        public async Task<T?> GetByConditionsAsync(
            Expression<Func<T, bool>> condition,
            bool trackChanges = true,
            params Expression<Func<T, object>>[] includes)
        {
            var entity = _dbContext.Set<T>().IgnoreAutoIncludes();

            entity = entity.IncludeOneOrMany(includes);

            entity = entity.TrackChanges(trackChanges);

            return await entity.FirstOrDefaultAsync(condition);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
           return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> CreateAsync(T entity)
        {
            var result = await _dbContext.Set<T>().AddAsync(entity);
            return result.Entity;
        }

        public async Task<T> Delete(int id)
        {
            var entity = await GetByIdAsync(id);

            ReturnKeyNotFoundExceptionWhenNull(entity);

           return _dbContext.Set<T>().Remove(entity!).Entity;
        }

        public async Task<T> Delete(T entity)
        {
            ReturnKeyNotFoundExceptionWhenNull(entity);
            return _dbContext.Set<T>().Remove(entity).Entity;
        }

        public async Task<T> Update(int Id, T updatedEntity)
        {
            var entity = await GetByIdAsync(Id);

            ReturnKeyNotFoundExceptionWhenNull(entity);
            return _dbContext.Set<T>().Update(entity!).Entity;
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

        public async Task<IQueryable<T>> GetAsQueryable(bool trackChanges = true)
        {
            var entity = _dbContext.Set<T>();

            return entity.TrackChanges(trackChanges);
        }

        private static void ReturnKeyNotFoundExceptionWhenNull(T? entity)
        {
            if (entity is null)
            {
                throw new KeyNotFoundException($"The {typeof(T).Name} Not Found With The Given Id");
            }
        }
    }
}
