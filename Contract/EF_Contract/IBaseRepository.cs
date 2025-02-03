using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.EF_Contract
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);

        Task<T> GetByConditionsAsync(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes);

        Task<IQueryable<T>> GetAsQueryable();


        Task<IEnumerable<T>> GetRangeAsync(params Expression<Func<T, object>>[]? includes);

        Task<IEnumerable<T>> GetRangeByConditionsAsync(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetRangeByConditionsAsync(Expression<Func<T, bool>>[]? condition, params Expression<Func<T, object>>[] includes);

        Task<T> Delete(int id);

        Task<T> Delete(T entity);

        Task<T> Update(int Id, T updatedEntity);
        Task<T> Update(T updatedEntity);
        Task<T> CreateAsync(T entity);

    }
}
