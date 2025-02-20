using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ServiceResultsDtos;
using CollegeUnity.Core.Dtos.VoteDtos;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.Services_Contract.ServiceAbstraction
{
    public interface IVoteService
    {
        Task<RequestResult<bool>> VoteInPost(VoteInPostDto dto);
        Task<GetPostVotesResultDto> GetPostVotes(GetPostVotesParameters parameters);
    }
}
