using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StaffFeatures.Posts;
using CollegeUnity.Contract.StaffFeatures.Posts.PostFiles;
using CollegeUnity.Contract.StaffFeatures.Posts.PostsVotes;
using CollegeUnity.Core.Entities;
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
        private readonly IPostFilesFeatures _postFilesFeatures;
        private readonly IPostVoteFeatures _postVoteFeatures;
        protected readonly IRepositoryManager _repositoryManager;

        public BasePost(IRepositoryManager repositoryManager, IPostFilesFeatures postFilesFeatures, IPostVoteFeatures postVoteFeatures)
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
            bool isExist = await _repositoryManager.PostRepository.isMyPost(staffId, postId);

            if (isExist)
            {
                Post post = await _repositoryManager.PostRepository.GetByIdAsync(postId);
                await _repositoryManager.PostRepository.Delete(post);
                return true;
            }
            return false;
        }
    }
}
