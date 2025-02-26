using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Contract.SharedFeatures.Posts;
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
    public class GetPublicPostFeatures : IGetPublicPostFeatures
    {
        private readonly IRepositoryManager _repositoryManager;
        public GetPublicPostFeatures(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public async Task<IEnumerable<GPublicPostDto>> GetPublicPostAsync(PublicPostParameters postParameters)
        {
            IEnumerable<Post> posts = await _repositoryManager.PostRepository.GetRangeByConditionsAsync(
                p => p.IsPublic == true,
                postParameters,
                [
                    i => i.PostFiles,
                    i => i.Staff
                ]
            );

            return posts.ToGPostMappers<GPublicPostDto>();
        }
    }
}
