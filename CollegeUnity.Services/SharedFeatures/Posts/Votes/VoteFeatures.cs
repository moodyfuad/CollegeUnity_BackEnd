using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.SharedFeatures.Posts.Votes;
using CollegeUnity.Core.CustomExceptions;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ServiceResultsDtos;
using CollegeUnity.Core.Dtos.VoteDtos;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.SharedFeatures.Posts.Votes
{
    public class VoteFeatures(IRepositoryManager _repositoryManager) : IVoteFeatures
    {
        public async Task<GetPostVotesResultDto> GetPostVotes(GetPostVotesParameters parameters)
        {
            var result = await _repositoryManager.VotesRepository.GetRangeByConditionsAsync(
                v => v.PostId == parameters.postId,
                parameters,
                v => v.Post,
                v => v.SelectedBy);

            return GetPostVotesResultDto.New(result.FirstOrDefault()?.Post ?? null);
        }

        public async Task<RequestResult<bool>> VoteInPost(int userId, VoteInPostDto dto)
        {
            var votes = await _repositoryManager.VotesRepository.GetPostVotes(dto.postId);

            if (votes.FirstOrDefault(v => v?.Id == dto.voteId, null) == null)
            {
                throw new BadRequestException("Vote Doesn't Exist");
            }

            User user = await _repositoryManager.UserRepository.GetByIdAsync(userId);

            foreach (PostVote vote in votes)
            {
                if (vote.Id == dto.voteId && vote.SelectedBy?.FirstOrDefault(u => u.Id == userId) == null)
                {
                    vote.SelectedBy?.Add(user!);
                }
                else if(vote.SelectedBy?.FirstOrDefault(u => u.Id == userId) == null)
                {
                    continue;
                }
                else
                {
                    vote.SelectedBy?.Remove(user!);
                }

                 await _repositoryManager.VotesRepository.Update(vote);
            }

            await _repositoryManager.SaveChangesAsync();

            return RequestResult<bool>.Success("Successfully Voted");
        }

        private RequestResult<bool> ValidatePost(Post post, PostVote vote)
        {
            if (post == null)
            {
                return RequestResult<bool>.Failed("post not found");
            }

            if (!post.Votes.Any())
            {
                return RequestResult<bool>.Failed("post does not contain any votes");
            }

            if (!post.Votes.Contains(vote))
            {
                return RequestResult<bool>.Failed("post does not contain selected vote");
            }

            return RequestResult<bool>.Success("");
        }

        private async Task<RequestResult<bool>> UpdateUserVote(PostVote newVoteSelection, User user, PostVote previousSelection)
        {
            previousSelection.SelectedBy?.Remove(user);
            await _repositoryManager.VotesRepository.Update(previousSelection);
            newVoteSelection.SelectedBy?.Add(user);
            await _repositoryManager.VotesRepository.Update(newVoteSelection);
            await _repositoryManager.SaveChangesAsync();
            return RequestResult<bool>.Success("change Voted");
        }
    }
}
