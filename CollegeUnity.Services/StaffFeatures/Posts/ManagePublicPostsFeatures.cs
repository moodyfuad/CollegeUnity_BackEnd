using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.EF_Contract.IEntitiesRepository;
using CollegeUnity.Contract.StaffFeatures.Posts.PostFiles;
using CollegeUnity.Contract.StaffFeatures.Posts.PostsVotes;
using CollegeUnity.Core.Dtos.PostDtos.Create;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.MappingExtensions.PostExtensions.Create;
using CollegeUnity.Services.StaffFeatures.Posts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StaffFeatures.Posts
{
    public class ManagePublicPostsFeatures : BasePost, IManagePublicPostsFeatures
    {
        public ManagePublicPostsFeatures(
            IRepositoryManager repositoryManager, 
            IPostFilesFeatures postFilesFeatures,
            IPostVoteFeatures postVoteFeatures) : base(repositoryManager, postFilesFeatures, postVoteFeatures)
        {
        }

        public async Task CreatePublicPostAsync(CPublicPostDto dto)
        {
            Post post = dto.ToPost<Post>();
            post = await _repositoryManager.PostRepository.CreateAsync(post);
            await _repositoryManager.SaveChangesAsync();

            if (dto.PictureFiles != null)
            {
                await createPostFiles(dto.PictureFiles, post.Id);
            }

            if (dto.Votes != null)
            {
                await createPostVotes(dto.Votes, post.Id);
            }
        }
    }
}
