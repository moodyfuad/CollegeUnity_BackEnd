using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Core.Dtos.ServiceResultsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollegeUnity.Core.Dtos.VoteDtos;
using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Dtos.QueryStrings;


namespace CollegeUnity.Services.VoteServices
{
    public partial class VoteService : IVoteService
    {
        private readonly IRepositoryManager _repositoryManager;

        public VoteService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<RequestResult<bool>> VoteInPost(VoteInPostDto dto)
        {
            return await _VoteInPost(dto);
        }

        public async Task<GetPostVotesResultDto> GetPostVotes(GetPostVotesParameters parameters)
        {
            var result = await _repositoryManager.VotesRepository.GetRangeByConditionsAsync(
                v => v.PostId == parameters.postId,
                parameters,
                v => v.Post,
                v => v.SelectedBy);

            return GetPostVotesResultDto.New(result.FirstOrDefault()?.Post ?? null);
        }
    }
}
