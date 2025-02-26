using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Contract.SharedFeatures.Posts;
using CollegeUnity.Contract.StudentFeatures.Subject;
using CollegeUnity.Core.Dtos.PostDtos.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.MappingExtensions.PostExtensions.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.SharedFeatures.Posts
{
    public class GetSubjectPostFeatures : IGetSubjectPostFeatures
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IStudentSubjectFeatures _studentSubjectFeatures;
        public GetSubjectPostFeatures(IRepositoryManager repositoryManager, IStudentSubjectFeatures studentSubjectFeatures)
        {
            _repositoryManager = repositoryManager;
            _studentSubjectFeatures = studentSubjectFeatures;
        }
        public async Task<IEnumerable<GStudentBatchPost>> GetSubjectPosts(SubjectPostParameters parameters)
        {
            List<int> subjects = await _studentSubjectFeatures.GetStudentSubject(parameters.ForLevel, parameters.ForMajor, parameters.ForAcceptanceType);
            IEnumerable<Post> posts = await _repositoryManager.PostRepository.GetRangeByConditionsAsync(
                p => subjects.Contains((int)p.SubjectId),
                parameters,
                [
                    i => i.PostFiles,
                    i => i.Staff,
                    i => i.Subject
                ]);
            return posts.ToGPostMappers<GStudentBatchPost>();
        }
    }
}
