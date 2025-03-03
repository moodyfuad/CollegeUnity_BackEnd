using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ServiceResultsDtos;
using CollegeUnity.Core.Dtos.VoteDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.SharedFeatures.Posts.Votes
{
    public interface IVoteFeatures
    {
        Task<RequestResult<bool>> VoteInPost(VoteInPostDto dto);

        Task<GetPostVotesResultDto> GetPostVotes(GetPostVotesParameters parameters);
    }
}
