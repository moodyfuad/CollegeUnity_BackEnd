//using CollegeUnity.Core.Dtos.ServiceResultsDtos;
//using CollegeUnity.Core.Dtos.VoteDtos;
//using CollegeUnity.Core.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection.Metadata.Ecma335;
//using System.Text;
//using System.Threading.Tasks;

//namespace CollegeUnity.Services.VoteServices
//{
//    public partial class VoteService
//    {
//        public async Task<RequestResult<bool>> _VoteInPost(VoteInPostDto dto)
//        {
//            var post = await _repositoryManager.PostRepository.GetByConditionsAsync(
//                condition: p => p.Id == dto.postId,
//                includes: p => p.Votes);

//            PostVote vote = await GetVoteById(dto);

//            User user = null;

//            if (user == null)
//            {
//                return RequestResult<bool>.Failed("user not found");
//            }

//            var validateResult = ValidatePost(post, vote);

//            if (!validateResult.IsSuccess)
//            {
//                return validateResult;
//            }

//            if (vote == null)
//            {
//                return RequestResult<bool>.Failed("vote not found");
//            }

//            PostVote previousSelection = IsUserAlreadyVoted(user, post);

//            if (previousSelection != null)
//            {
//                return await UpdateUserVote(vote, user, previousSelection);
//            }

//            try
//            {
//                vote.SelectedBy?.Add(user);
//                await _repositoryManager.VotesRepository.Update(vote);
//                await _repositoryManager.SaveChangesAsync();

//                return RequestResult<bool>.Success("Voted Successfully");
//            }
//            catch (Exception ex)
//            {
//                return RequestResult<bool>.Failed(ex.Message);
//            }
//        }

//        private RequestResult<bool> ValidatePost(Post post, PostVote vote)
//        {
//            if (post == null)
//            {
//                return RequestResult<bool>.Failed("post not found");
//            }

//            if (!post.Votes.Any())
//            {
//                return RequestResult<bool>.Failed("post does not contain any votes");
//            }

//            if (!post.Votes.Contains(vote))
//            {
//                return RequestResult<bool>.Failed("post does not contain selected vote");
//            }

//            return RequestResult<bool>.Success("");
//        }

//        private async Task<RequestResult<bool>> UpdateUserVote(PostVote newVoteSelection, User user, PostVote previousSelection)
//        {
//            previousSelection.SelectedBy?.Remove(user);
//            await _repositoryManager.VotesRepository.Update(previousSelection);
//            newVoteSelection.SelectedBy?.Add(user);
//            await _repositoryManager.VotesRepository.Update(newVoteSelection);
//            await _repositoryManager.SaveChangesAsync();
//            return RequestResult<bool>.Success("change Voted");
//        }

//        private async Task<PostVote> GetVoteById(VoteInPostDto dto)
//        {
//            return await _repositoryManager.VotesRepository.GetByConditionsAsync(
//                condition: v => v.Id == dto.voteId,
//                includes: v => v.SelectedBy);
//        }

        

//        PostVote? IsUserAlreadyVoted(User user, Post post)
//        {
//            var userSelectedVote = post.Votes?
//                .FirstOrDefault(v => v.SelectedBy?.Contains(user) ?? default) ?? null;

//            return userSelectedVote;
//        }
//    }
//}
