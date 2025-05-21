using CollegeUnity.Contract.AdminFeatures.Subjects;
using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.SubjectDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions;
using CollegeUnity.Core.MappingExtensions.SubjectExtenstions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.StaffFeatures.Subjects
{
    public class GetMySubjects : IGetMySubjects
    {
        private readonly IRepositoryManager _repositoryManager;

        public GetMySubjects(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<List<DistinctSubjectDto>> MySubjects(int teacherId)
        {
            return await _repositoryManager.SubjectRepository.GetStaffDistinctSubjects(teacherId);
        }

        public async Task<PagedList<GTeacherSubjectDto>> MySubjects(int teacherId, GetMySubjectsParameters parameters)
        {
            var subjects = await _repositoryManager.SubjectRepository.GetRangeByConditionsAsync(s => s.TeacherId == teacherId, parameters);
            return subjects.MapPagedList(SubjectExtention.GetTeacherSubject);
        }

        public async Task<List<int>> GetSubjectsBy(Level level, Major major, AcceptanceType acceptanceType)
        {
            return await _repositoryManager.SubjectRepository.GetDistinctSubjects(level, major, acceptanceType);
        }
    }
}
