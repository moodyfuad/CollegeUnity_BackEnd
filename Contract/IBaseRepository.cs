using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract
{
    public interface IBaseRepository<T>  where T : class
    {
        Task<IQueryable<T>> GetAsync(Guid id);
        Task<IQueryable<T>> GetByConditionsAsync(params Expression<Func<T,bool>>[] conditions);
        Task<IQueryable<T>> GetAllAsync();
        Task<IQueryable<T>> GetAllByConditionsAsync(params Expression<Func<T, bool>>[] conditions);
        Task<T> DeleteAsync(Guid id);
        Task<T> DeleteByConditionsAsync(params Expression<Func<T, bool>>[] conditions);
        Task<T> UpdateAsync(T entity, T updatedEntity);
        Task<T> CreateAsync(T entity);

    }
}
