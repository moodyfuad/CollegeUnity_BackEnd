using CollegeUnity.Contract.EF_Contract.IEntitiesRepository;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.EF.Repositories.EntitiesRepository
{
    public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository
    {
        private readonly CollegeUnityDbContext _dbContext;
        public SubjectRepository(CollegeUnityDbContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }

        public async Task<List<int>> GetDistinctSubjects(Level level, Major major, AcceptanceType acceptanceType)
        {
            return _dbContext.Subjects
            .Where(s => s.Level == level && s.Major == major && s.AcceptanceType == acceptanceType)
            .Select(s => s.Id)
            .Distinct()
            .ToList();
        }

        public async Task<PagedList<Subject>?> GetIntresetedSubject(int studentId, GetInterestedSubjectParameters getInterestedSubjectParameters)
        {
            var student = await _dbContext.Students
                .Include(s => s.InterestedSubjects)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            var subjectIds = student.InterestedSubjects.Select(s => s.Id).ToList();

            Expression<Func<Subject, bool>> condition = s => subjectIds.Contains(s.Id);

            return await GetRangeByConditionsAsync(
                condition,
                getInterestedSubjectParameters
            );
        }

        public async Task<bool> IsExistById(int id)
        {
            return await _dbContext.Subjects.AnyAsync(s => s.Id == id);
        }
    }
}
