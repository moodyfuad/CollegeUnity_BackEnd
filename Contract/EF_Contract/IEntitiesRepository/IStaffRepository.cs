using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.EF_Contract.IEntitiesRepository
{
    public interface IStaffRepository : IBaseRepository<Staff>
    {
        Task<bool> IsExistById(int id);
        Task<Staff> GetByEmail(string email);
        Task<bool> GetByConditions(Expression<Func<Staff, bool>> expression);
    }
}
