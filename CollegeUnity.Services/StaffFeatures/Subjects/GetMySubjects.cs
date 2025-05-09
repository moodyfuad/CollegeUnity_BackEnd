using CollegeUnity.Contract.AdminFeatures.Subjects;
using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Core.Dtos.SubjectDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
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
    }
}
