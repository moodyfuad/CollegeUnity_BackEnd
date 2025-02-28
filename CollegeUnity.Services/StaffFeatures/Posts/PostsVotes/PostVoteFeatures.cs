using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StaffFeatures.Posts.PostsVotes;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.PostVotesExtensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.StaffFeatures.Posts.PostsVotes
{
    public class PostVoteFeatures : IPostVoteFeatures
    {
        private readonly IRepositoryManager _repositoryManager;
        
        public PostVoteFeatures(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<bool> AddVotesToPost(ICollection<string> votes, int postId)
        {
            var isExist = await _repositoryManager.PostRepository.isExist(postId);
            
            if (isExist)
            {
                IEnumerable<PostVote> postVotes = await MappingStringToVote(votes, postId);
                await _repositoryManager.VotesRepository.AddRangeAsync(postVotes);
                await _repositoryManager.SaveChangesAsync();
                return true;
            }
            return false;
        }

        private async Task<IEnumerable<PostVote>> MappingStringToVote(IEnumerable<string> votes, int postId)
        {
            List<PostVote> postVotes = new List<PostVote>();
            foreach (var vote in votes)
            {
                PostVote postVote = vote.ToPostVote<PostVote>(postId);
                postVotes.Add(postVote);
            }
            return postVotes;
        }
    }
}
