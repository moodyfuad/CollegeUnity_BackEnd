using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StaffFeatures.Posts;
using CollegeUnity.Contract.StaffFeatures.Posts.PostFiles;
using CollegeUnity.Contract.StaffFeatures.Posts.PostsVotes;
using CollegeUnity.Core.Dtos.PostDtos.Update;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.PostExtensions.Update;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.StaffFeatures.Posts
{
    public class BasePost : IBasePost
    {
        private readonly IFilesFeatures _postFilesFeatures;
        private readonly IPostVoteFeatures _postVoteFeatures;
        protected readonly IRepositoryManager _repositoryManager;

        public BasePost(IRepositoryManager repositoryManager, IFilesFeatures postFilesFeatures, IPostVoteFeatures postVoteFeatures)
        {
            _repositoryManager = repositoryManager;
            _postFilesFeatures = postFilesFeatures;
            _postVoteFeatures = postVoteFeatures;
        }

        public async Task createPostFiles(List<IFormFile> pictureFiles, int postId)
        {
            await _postFilesFeatures.CreatePostFiles(pictureFiles, postId);
        }

        public async Task createPostVotes(List<string> votes, int postId)
        {
            await _postVoteFeatures.AddVotesToPost(votes, postId);
        }

        public async Task<bool> DeleteAsync(int staffId, int postId)
        {
            //Check if the post belongs to the staff
            Post post = await _repositoryManager.PostRepository.GetByConditionsAsync(
                i => i.Id == postId && i.StaffId == staffId,
                p => p.PostFiles
            );

            if (post != null)
            {
                await _repositoryManager.PostRepository.Delete(post);
                await _repositoryManager.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdatePostAsync(int postId, int staffId, UUpdatePostDto dto)
        {
            //Check if the post belongs to the staff
            Post post = await _repositoryManager.PostRepository.GetByConditionsAsync(
                i => i.Id == postId && i.StaffId == staffId,
                p => p.PostFiles
            );

            if (post != null)
            {
                post = post.GetPostUpdated(dto);
                var picturesToKeep = post.PostFiles.Where(p => dto.ExistingPictureIds.Contains(p.Id)).ToList();

                foreach (var picture in picturesToKeep)
                {
                    post.PostFiles.Remove(picture);
                    await _repositoryManager.PostFilesRepository.Delete(picture);
                }

                await createPostFiles(dto.NewPictures, postId);
                await _repositoryManager.PostRepository.Update(post);
                await _repositoryManager.SaveChangesAsync();
                return true;
            }
            return false;

        }
    }
}
