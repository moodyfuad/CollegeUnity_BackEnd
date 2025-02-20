using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.VoteDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeUnity.API.Controllers.Vote
{
    [Route("api/")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IServiceManager _services;

        public VoteController(IServiceManager services)
        {
            _services = services;
        }

        [HttpPost("posts/votes/user")]
        public async Task<IActionResult> AddVoteToPost([FromQuery] VoteInPostDto dto)
        {
            var result = await _services.VoteService.VoteInPost(dto);

            if (result.IsSuccess)
            {
                var response = ApiResponse<bool>.Success(true, result.Message);
                return new JsonResult(response);
            }
            else
            {
                var response = ApiResponse<bool>.BadRequest(result.Message);
                return new JsonResult(response);
            }
        }

        [HttpGet("posts/votes")]
        public async Task<IActionResult> GetVotes([FromQuery] GetPostVotesParameters parameters)
        {
            var result = await _services.VoteService.GetPostVotes(parameters);

            return new JsonResult(result);
        }
    }
}
