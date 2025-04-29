using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Contract.StudentFeatures.Subjects;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.InterestedSubjectDtos;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.StudentFeatures;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.InterestedSubjectExtensions;
using CollegeUnity.Core.MappingExtensions.StudentExtensions.Get;
using Microsoft.Extensions.Options;
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

        public async Task<PagedList<GInterestedSubjectDto>?> GetStudentIntrestedSubject(GetInterestedSubjectParameters parameters, int studentId)
        {
            var subjects = await _repositoryManager.SubjectRepository.GetIntresetedSubject(studentId, parameters);
            return subjects.toInterestedSubject();
        }

        public async Task<List<int>> GetStudentSubject(Level level, Major major, AcceptanceType acceptanceType)
        {
            return await _repositoryManager.SubjectRepository.GetDistinctSubjects(level, major, acceptanceType);
        }

        public async Task<PagedList<GStudentSubjectsDto>> GetStudentSubjectWithNames(GetFilterBatchPostParameters parameters)
        {
            var studentSubjectsId = await GetStudentSubject(parameters.Level, parameters.Major, parameters.AcceptanceType);
            var subjects = await _repositoryManager.SubjectRepository.GetRangeByConditionsAsync(c => studentSubjectsId.Contains(c.Id), parameters);
            return subjects.GetStudentSubjectsNames();
        }

        public async Task<ResultDto> MakeSubjectInterest(int studentId, int subjectId)
        {
            var isStudentExist = await _repositoryManager.StudentRepository.ExistsAsync(studentId);
            var isSubjectExist = await _repositoryManager.SubjectRepository.ExistsAsync(subjectId);

            if (!isStudentExist || !isSubjectExist)
            {
                return new(false, "Something wrong!");
            }

            var isAlreadyExist = await _repositoryManager.SubjectRepository.IsInterestedSubjectExist(studentId, subjectId);

            if (isAlreadyExist)
            {
                return new(false, "You already interest in this subject.");
            }

            await _repositoryManager.SubjectRepository.MakeSubjectInterest(studentId, subjectId);
            await _repositoryManager.SaveChangesAsync();

            return new(true);

        }

        public async Task<ResultDto> MakeSubjectUnInterested(int studentId, int subjectId)
        {
            var isStudentExist = await _repositoryManager.StudentRepository.ExistsAsync(studentId);
            var isSubjectExist = await _repositoryManager.SubjectRepository.ExistsAsync(subjectId);

            if (!isStudentExist || !isSubjectExist)
            {
                return new(false, "Something wrong!");
            }

            var isAlreadyExist = await _repositoryManager.SubjectRepository.IsInterestedSubjectExist(studentId, subjectId);

            if (!isAlreadyExist)
            {
                return new(false, "You don't interest in this subject.");
            }

            await _repositoryManager.SubjectRepository.MakeSubjectUnInterested(studentId, subjectId);
            await _repositoryManager.SaveChangesAsync();

            return new(true);

        }
    }
}
