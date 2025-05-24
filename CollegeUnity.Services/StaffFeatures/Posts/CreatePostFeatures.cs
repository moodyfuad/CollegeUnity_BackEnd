using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StaffFeatures.Posts;
using CollegeUnity.Contract.StaffFeatures.Posts.PostFiles;
using CollegeUnity.Contract.StaffFeatures.Posts.PostsVotes;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.PostDtos.Create;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.StaffFeatures.Posts
{
    public class CreatePostFeatures : BasePost, ICreatePostFeatures
    {
        public CreatePostFeatures(
            IRepositoryManager repositoryManager, 
            IFilesFeatures postFilesFeatures, 
            IPostVoteFeatures postVoteFeatures) : base(repositoryManager, postFilesFeatures, postVoteFeatures)
        {
        }

        public async Task<ResultDto> CreatePostAsync<TDto>(TDto dto, int staffId) where TDto : CPostDto
        {
            Post post = dto.ToPost(staffId);
            post = await _repositoryManager.PostRepository.CreateAsync(post);
            await _repositoryManager.SaveChangesAsync();

            if (dto.PictureFiles != null)
            {
                var isSuccess = ValidatePostFiles(dto.PictureFiles);
                if (!isSuccess.success)
                    return isSuccess;

                await createPostFiles(dto.PictureFiles, post.Id);
            }

            if (dto.Votes != null)
            {
                await createPostVotes(dto.Votes, post.Id);
            }

            return new(true, null);
        }
    }
}
