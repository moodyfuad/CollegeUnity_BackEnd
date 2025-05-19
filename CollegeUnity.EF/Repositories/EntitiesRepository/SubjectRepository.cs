using CollegeUnity.Contract.EF_Contract.IEntitiesRepository;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.SubjectDtos;
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

        public async Task<List<DistinctSubjectDto>> GetStaffDistinctSubjects(int staffId)
        {
            return await _dbContext.Subjects
            .Where(s => s.TeacherId == staffId)
            .Select(s => new DistinctSubjectDto
            {
                Level = s.Level,
                Major = s.Major,
                AcceptanceType = s.AcceptanceType
            })
            .Distinct()
            .ToListAsync();
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
                getInterestedSubjectParameters,
                false,
                s => s.Teacher
            );
        }

        public async Task MakeSubjectInterest(int studentId, int subjectId)
        {
            var student = await _dbContext.Students
                .Include(s => s.InterestedSubjects)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            var subject = await _dbContext.Subjects.FirstOrDefaultAsync(s => s.Id == subjectId);

            student!.InterestedSubjects!.Add(subject!);
        }

        public async Task MakeSubjectUnInterested(int studentId, int subjectId)
        {
            var student = await _dbContext.Students
                .Include(s => s.InterestedSubjects)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            var subject = await _dbContext.Subjects.FirstOrDefaultAsync(s => s.Id == subjectId);

            student!.InterestedSubjects!.Remove(subject!);
        }

        public async Task<bool> IsInterestedSubjectExist(int studentId, int subjectId)
        {
            var subject = await _dbContext.Subjects.Include(i => i.InterestedStudents).FirstOrDefaultAsync(s => s.Id == subjectId);

            var isAlreadyExist = subject.InterestedStudents.Any(s => s.Id == studentId);
            if (isAlreadyExist)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsExistById(int id)
        {
            return await _dbContext.Subjects.AnyAsync(s => s.Id == id);
        }
    }
}
