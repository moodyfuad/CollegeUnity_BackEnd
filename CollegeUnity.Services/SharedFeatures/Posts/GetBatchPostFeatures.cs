using CollegeUnity.Contract.AdminFeatures.Subjects;
using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Contract.SharedFeatures.Posts;
using CollegeUnity.Contract.StudentFeatures.Subjects;
using CollegeUnity.Core.Dtos.PostDtos.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.PostExtensions.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.SharedFeatures.Posts
{
    public class GetBatchPostFeatures : IGetBatchPostFeatures
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IStudentSubjectFeatures _studentSubjectFeatures;
        private readonly IGetMySubjects _getMySubjects;
        public GetBatchPostFeatures(IRepositoryManager repositoryManager, IStudentSubjectFeatures studentSubjectFeatures, IGetMySubjects getMySubjects)
        {
            _repositoryManager = repositoryManager;
            _studentSubjectFeatures = studentSubjectFeatures;
            _getMySubjects = getMySubjects;
        }

        public async Task<PagedList<GStudentBatchPost>> GetBatchPost(int studentId, SubjectPostParameters parameters)
        {
            List<int> subjects;
            bool hasFilters = parameters.Level != null ||
                  parameters.Major != null ||
                  parameters.AcceptanceType != null;

            if (hasFilters)
                subjects = await _getMySubjects.GetSubjectsBy(parameters.Level.Value, parameters.Major.Value, parameters.AcceptanceType.Value);
            else
            {
                Student student = await _repositoryManager.StudentRepository.GetByIdAsync(studentId);
                subjects = await _studentSubjectFeatures.GetStudentSubject(student.Level, student.Major, student.AcceptanceType);
            }

            PagedList<Post> posts = await _repositoryManager.PostRepository.GetVotesWithConditionsAsync(
                p => subjects.Contains((int)p.SubjectId),
                parameters,
                false,
                i => i.PostFiles,
                i => i.Staff,
                i => i.Subject,
                i => i.Votes
            );
            return posts.ToGPostMappers<GStudentBatchPost>();
        }
    }
}
