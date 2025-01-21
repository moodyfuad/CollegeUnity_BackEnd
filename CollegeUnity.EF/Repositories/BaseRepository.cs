using CollegeUnity.Contract;
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

        public async Task<T> CreateAsync(T entity)
        {
            _dbContext.Set<T>();
            throw new NotImplementedException();
        }

        public async Task<T> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<T> DeleteByConditionsAsync(params Expression<Func<T, bool>>[] conditions)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<T>> GetAllByConditionsAsync(params Expression<Func<T, bool>>[] conditions)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<T>> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<T>> GetByConditionsAsync(params Expression<Func<T, bool>>[] conditions)
        {
            throw new NotImplementedException();
        }

        public async Task<T> UpdateAsync(T entity, T updatedEntity)
        {
            throw new NotImplementedException();
        }
    }
}
