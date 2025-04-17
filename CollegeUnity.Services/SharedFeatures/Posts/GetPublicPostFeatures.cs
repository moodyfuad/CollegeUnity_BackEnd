using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Contract.SharedFeatures.Posts;
using CollegeUnity.Core.Dtos.PostDtos.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
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
        public async Task<PagedList<GPublicPostDto>> GetPublicPostAsync(PublicPostParameters postParameters)
        {
            int filterNumber = (int)postParameters.FilterPost;
            PagedList<Post> posts = await _repositoryManager.PostRepository.GetRangeByConditionsAsync(
                p => p.IsPublic == true &&
                filterNumber == 0 || p.Staff.Roles.Contains((Roles)filterNumber),
                postParameters,
                [
                    i => i.PostFiles,
                    i => i.Staff,
                    i => i.Votes
                ]
            );

            return posts.ToGPostMappers<GPublicPostDto>();
        }
    }
}
