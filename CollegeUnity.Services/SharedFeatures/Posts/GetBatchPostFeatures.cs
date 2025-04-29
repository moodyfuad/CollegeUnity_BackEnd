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
        public GetBatchPostFeatures(IRepositoryManager repositoryManager, IStudentSubjectFeatures studentSubjectFeatures)
        {
            _repositoryManager = repositoryManager;
            _studentSubjectFeatures = studentSubjectFeatures;
        }

        public async Task<PagedList<GStudentBatchPost>> GetBatchPost(int studentId, SubjectPostParameters parameters)
        {
            Student student = await _repositoryManager.StudentRepository.GetByIdAsync(studentId);
            List<int> subjects = await _studentSubjectFeatures.GetStudentSubject(student.Level, student.Major, student.AcceptanceType);
            PagedList<Post> posts = await _repositoryManager.PostRepository.GetRangeByConditionsAsync(
                p => subjects.Contains((int)p.SubjectId),
                parameters,
                [
                    i => i.PostFiles,
                    i => i.Staff,
                    i => i.Subject,
                    i => i.Votes
                ]);
            return posts.ToGPostMappers<GStudentBatchPost>();
        }
    }
}
