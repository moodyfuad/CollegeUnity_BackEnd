 using CollegeUnity.Contract.AdminFeatures.Student;
using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.StudentFeatures;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.StudentExtensions.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.AdminFeatures.Students
{
    public class ManageStudentFeatures : IManageStudentFeatures
    {
        private readonly IRepositoryManager _repositoryManager;
        public ManageStudentFeatures(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<PagedList<GStudentDto>> GetStudents(GetStudentParameters parameters)
        {
            bool isNumber = double.TryParse(parameters.NameOrCardId, out _);

            Expression<Func<Student, bool>> conditions = s =>
                (parameters.Level == default || s.Level == parameters.Level) &&
                (parameters.AccountStatus == null || s.AccountStatus == parameters.AccountStatus) &&
                (string.IsNullOrEmpty(parameters.NameOrCardId) ||
                    (isNumber ? s.CardId.StartsWith(parameters.NameOrCardId)
                              : string.Concat(s.FirstName.ToLower(), " ", s.LastName.ToLower()).StartsWith(parameters.NameOrCardId)));

            var students = await _repositoryManager.StudentRepository.GetRangeByConditionsAsync(conditions, parameters);

            return students.ToGetStudents();
        }

        public async Task<PagedList<GStudentDto>> GetStudentSignUpRequest(GetStudentParameters parameters)
        {
            Expression<Func<Student, bool>> conditions = s => s.AccountStatus == Core.Enums.AccountStatus.Waiting;

            var students = await _repositoryManager.StudentRepository.GetRangeByConditionsAsync(conditions, parameters);

            return students.ToGetStudents();
        }
    }
}
