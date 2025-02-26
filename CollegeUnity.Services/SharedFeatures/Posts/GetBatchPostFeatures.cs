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
    public class GetBatchPostFeatures : IGetBatchPostFeatures
    {
        private readonly IRepositoryManager _repositoryManager;
        public GetBatchPostFeatures(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public async Task<IEnumerable<GBatchPostDto>> GetPublicAndBatchPostAsync(PublicAndBatchPostParameters batchPostParameters)
        {
            IEnumerable<Post> posts = await _repositoryManager.PostRepository.GetRangeByConditionsAsync(
                p => p.IsPublic == true && p.ForMajor == batchPostParameters.ForMajor &&
                p.ForLevel == batchPostParameters.ForLevel &&
                p.ForAcceptanceType == batchPostParameters.ForAcceptanceType,
                batchPostParameters,
                [
                    i => i.PostFiles,
                    i => i.Staff
                ]);
            return posts.ToGPostMappers<GBatchPostDto>();
        }
    }
}
