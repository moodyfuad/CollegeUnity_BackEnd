using CollegeUnity.Contract.AdminFeatures.Subjects;
using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Contract.SharedFeatures.Posts;
using CollegeUnity.Contract.StudentFeatures.Subjects;
using CollegeUnity.Core.Dtos.PostDtos.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.PostExtensions.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            Level level;
            Major major;
            AcceptanceType acceptanceType;

            Student student = await _repositoryManager.StudentRepository.GetByIdAsync(studentId);

            if (student != null)
            {
                level = student.Level;
                major = student.Major;
                acceptanceType = student.AcceptanceType;
                subjects = await _studentSubjectFeatures.GetStudentSubject(level, major, acceptanceType);
            }
            else
            {
                level = parameters.Level ?? 0;
                major = parameters.Major ?? 0;
                acceptanceType = parameters.AcceptanceType ?? 0;
                subjects = await _getMySubjects.GetSubjectsBy(level, major, acceptanceType);
            }

            Expression<Func<Post, bool>> filter = p =>
                subjects.Contains((int)p.SubjectId) ||
                (
                    p.ForLevel == level &&
                    p.ForMajor == major &&
                    p.ForAcceptanceType == acceptanceType
                );

            var posts = await _repositoryManager.PostRepository.GetVotesWithConditionsAsync(
                filter,
                parameters,
                false,
                i => i.PostFiles,
                i => i.Staff,
                i => i.Subject,
                i => i.Votes
            );

            return posts.ToGPostMappers<GStudentBatchPost>(studentId);
        }

    }
}
