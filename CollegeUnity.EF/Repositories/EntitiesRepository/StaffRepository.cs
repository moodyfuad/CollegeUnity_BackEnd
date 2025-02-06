using CollegeUnity.Contract.EF_Contract.IEntitiesRepository;
using CollegeUnity.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.EF.Repositories.EntitiesRepository
{
    public class StaffRepository : BaseRepository<Staff>, IStaffRepository
    {
        private readonly CollegeUnityDbContext _context;
        public StaffRepository(CollegeUnityDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Staff> GetByEmail(string email)
        {
            return await GetByConditionsAsync(s => s.Email.ToLower().Equals(email));
        }

        public async Task<bool> IsExistById(int id)
        {
            return await _context.Staffs.AnyAsync(s => s.Id == id);
        }
    }
}
