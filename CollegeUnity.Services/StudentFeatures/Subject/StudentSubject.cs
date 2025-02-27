using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Contract.StudentFeatures.Subject;
using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.StudentFeatures.Subject
{
    public class StudentSubject : IStudentSubjectFeatures
    {
        private readonly IRepositoryManager _repositoryManager;
        public StudentSubject(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public async Task<List<int>> GetStudentSubject(Level level, Major major, AcceptanceType acceptanceType)
        {
            return await _repositoryManager.SubjectRepository.GetDistinctSubjects(level, major, acceptanceType);
        }
    }
}
