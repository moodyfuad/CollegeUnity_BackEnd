using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Contract.StudentFeatures.Subjects;
using CollegeUnity.Core.Dtos.InterestedSubjectDtos;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.MappingExtensions.InterestedSubjectExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.StudentFeatures.Subjects
{
    public class StudentSubjectFeatures : IStudentSubjectFeatures
    {
        private readonly IRepositoryManager _repositoryManager;
        public StudentSubjectFeatures(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<IEnumerable<GInterestedSubjectDto>?> GetStudentIntrestedSubject(GetInterestedSubjectParameters parameters, int studentId)
        {
            var subjects = await _repositoryManager.SubjectRepository.GetIntresetedSubject(studentId, parameters);
            return subjects.toInterestedSubject();
        }

        public async Task<List<int>> GetStudentSubject(Level level, Major major, AcceptanceType acceptanceType)
        {
            return await _repositoryManager.SubjectRepository.GetDistinctSubjects(level, major, acceptanceType);
        }
    }
}
