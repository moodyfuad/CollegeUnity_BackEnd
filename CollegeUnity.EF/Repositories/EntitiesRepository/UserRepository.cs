using CollegeUnity.Contract.EF_Contract.IEntitiesRepository;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
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

        public async Task<AccountStatus> GetAccountStatus(int userId)
        {
            var accountStatusParam = new SqlParameter
            {
                ParameterName = "@AccountStatus",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            var userIdParam = new SqlParameter("@Id", userId);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC GetAccountStatus @Id, @AccountStatus OUTPUT",
                userIdParam,
                accountStatusParam);

            int statusValue = (int)accountStatusParam.Value;

            return (AccountStatus)statusValue;
        }

    }
}
