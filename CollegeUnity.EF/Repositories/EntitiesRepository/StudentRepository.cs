using CollegeUnity.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollegeUnity.Core.Entities;
using CollegeUnity.Contract.EF_Contract.IEntitiesRepository;
using CollegeUnity.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using CollegeUnity.Core.Dtos.QueryStrings;

namespace CollegeUnity.EF.Repositories.EntitiesRepository
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        private readonly CollegeUnityDbContext _context;

        public StudentRepository(CollegeUnityDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<Student> GetByEmail(string email)
        {
            return await GetByConditionsAsync(s => s.Email.ToLower().Equals(email.ToLower()));
        }

        public async Task<string?> GetFullName(int id)
        {
            return await _context.Students
                .Where(s => s.Id == id)
                .Select(s => s.FirstName + " " + s.MiddleName + " " + s.LastName)
                .FirstOrDefaultAsync();
        }

        public async Task ChangeStateUpgradeLevelForStudents(bool isOpen)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC SetStudentsLevelEditable @Value = {0}", isOpen ? 1 : 0);
        }
    }
}
