 using CollegeUnity.Contract.AdminFeatures.Student;
using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.StudentFeatures;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.StudentExtensions.Get;
using LinqKit;
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

        public async Task<PagedList<GStudentDto>> GetStudentSignUpRequest(GetStudentSignUpParameters parameters)
        {
            Expression<Func<Student, bool>> conditions = c => c.AccountStatus == Core.Enums.AccountStatus.Waiting;
            bool isNumber = double.TryParse(parameters.NameOrCardId, out _);

            if (!string.IsNullOrEmpty(parameters.NameOrCardId))
            {
                conditions =
                    conditions.And(s => isNumber ? s.CardId == parameters.NameOrCardId :
                    (s.FirstName + " " + s.MiddleName + " " + s.LastName).StartsWith(parameters.NameOrCardId));
            }

            if (parameters.Level is not null)
            {
                conditions = conditions.And(s => s.Level == parameters.Level);
            }

            if (parameters.AcceptanceType is not null)
            {
                conditions = conditions.And(s => s.AcceptanceType == parameters.AcceptanceType);
            }

            if (parameters.Major is not null)
            {
                conditions = conditions.And(s => s.Major == parameters.Major);
            }


            var students = await _repositoryManager.StudentRepository.GetRangeByConditionsAsync(conditions, parameters);
            return students.ToGetStudents();
        }

        public async Task<ResultDto> OpenUpgradeStudentLevel(int studentId, Level level)
        {
            if (level < Level.First || level > Level.fourth)
                return new(false, "Invalid level. Level must be between 1 and 4.");
            var student = await _repositoryManager.StudentRepository.GetByIdAsync(studentId);
            if (student is null)
                return new(false, "Student not found.");
            student.Level = level;
            await _repositoryManager.StudentRepository.Update(student);
            await _repositoryManager.SaveChangesAsync();
            return new(true);
        }

        public async Task OpenUpgradeStudentsLevel(bool isOpen)
        {
            await _repositoryManager.StudentRepository.ChangeStateUpgradeLevelForStudents(isOpen);
        }
    }
}
