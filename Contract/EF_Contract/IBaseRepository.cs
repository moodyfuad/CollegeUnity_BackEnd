using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.EF_Contract
{
    public interface IBaseRepository<T>
        where T : class
    {
        Task<T?> GetByIdAsync(int id);

        Task<bool> ExistsAsync(int id);

        Task<T?> GetByConditionsAsync(Expression<Func<T, bool>> condition, bool trackChanges = true, params Expression<Func<T, object>>[] includes);

        Task<T?> GetByConditionsAsync(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes) => GetByConditionsAsync(condition, true, includes);

        Task<IQueryable<T>> GetAsQueryable(bool trackChanges = true);


        Task<PagedList<T>> GetRangeAsync(
            QueryStringParameters queryStringParameters,
            bool trackChanges = false,
            params Expression<Func<T, object>>[] includes);

        Task<PagedList<T>> GetRangeAsync(
            QueryStringParameters queryStringParameters,
            params Expression<Func<T, object>>[] includes) =>
                GetRangeAsync(queryStringParameters, false, includes);

        Task<PagedList<T>> GetRangeByConditionsAsync(
            Expression<Func<T, bool>> condition,
            QueryStringParameters queryStringParameters,
            bool trackChanges = false,
            params Expression<Func<T, object>>[] includes);

        Task<PagedList<T>> GetRangeByConditionsAsync(
            Expression<Func<T, bool>> condition,
            QueryStringParameters queryStringParameters,
            params Expression<Func<T, object>>[] includes) =>
                GetRangeByConditionsAsync(condition, queryStringParameters, false, includes);

        Task<PagedList<T>> GetRangeByConditionsAsync(
            Expression<Func<T, bool>>[] condition,
            QueryStringParameters queryStringParameters,
            bool trackChanges = false,
            params Expression<Func<T, object>>[] includes);

        Task<PagedList<T>> GetRangeByConditionsAsync(
            Expression<Func<T, bool>>[] condition,
            QueryStringParameters queryStringParameters,
            params Expression<Func<T, object>>[] includes) =>
            GetRangeByConditionsAsync(condition, queryStringParameters, false, includes);

        Task<T> Delete(int id);

        Task<T> Delete(T entity);

        Task<T> Update(int Id, T updatedEntity);

        Task<T> Update(T updatedEntity);

        Task<T> CreateAsync(T entity);
    }
}
