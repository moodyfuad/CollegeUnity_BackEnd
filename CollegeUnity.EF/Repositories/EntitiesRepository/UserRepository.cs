using CollegeUnity.Contract.EF_Contract.IEntitiesRepository;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.EF.Repositories.EntitiesRepository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly CollegeUnityDbContext _context;

        public UserRepository(CollegeUnityDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await GetByConditionsAsync(s => s.Email.ToLower().Equals(email.ToLower()),trackChanges:true);
        }
    }
}
