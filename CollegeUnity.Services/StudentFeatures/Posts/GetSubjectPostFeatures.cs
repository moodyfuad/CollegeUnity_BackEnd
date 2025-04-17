using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StudentFeatures.Post;
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

namespace CollegeUnity.Services.StudentFeatures.Posts
{
    public class GetSubjectPostFeatures : IGetSubjectPostFeatures
    {
        private readonly IRepositoryManager _repositoryManager;
        public GetSubjectPostFeatures(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<PagedList<GSubjectPostDto>> GetSubjectPosts(GetSubjectPostParameters parameters)
        {
            PagedList<Post> posts = await _repositoryManager.PostRepository.GetRangeByConditionsAsync(
                p => p.SubjectId == parameters.Id,
                parameters,
                [
                    i => i.PostFiles,
                    i => i.Staff,
                    i => i.Votes,
                    i => i.Subject
                ]
            );

            return posts.ToGPostMappers<GSubjectPostDto>();
        }
    }
}
