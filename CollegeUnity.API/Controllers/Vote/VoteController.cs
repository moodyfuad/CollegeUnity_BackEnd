using CollegeUnity.API.Filters;
using CollegeUnity.API.Middlerware_Extentions;
using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Contract.SharedFeatures.Posts.Votes;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.VoteDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace CollegeUnity.API.Controllers.Vote
{
    [Route("api/Post/")]
    [ApiController]
    [ServiceFilter(typeof(ActiveUserAttribute))]
    public class VoteController : ControllerBase
    {
        private readonly IVoteFeatures voteFeatures;
        //private readonly IServiceManager _serviceManager;

        public VoteController(IVoteFeatures voteFeatures)
        {
            this.voteFeatures = voteFeatures;
            //_serviceManager = serviceManager;
        }

        [HttpPost("Vote")]
        [ValidateEntityExist("postid")]
        public async Task<ActionResult<ApiResponse<bool>>> AddVoteToPost([FromQuery] VoteInPostDto dto)
        {
            int userId = User.GetUserId();
            var result = await voteFeatures.VoteInPost(userId, dto);

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

        [HttpGet("{postId}/Votes")]
        //[ServiceFilter(typeof(ValidateExistActionFilter))]
        [ValidateEntityExist("postid")]
        public async Task<IActionResult> GetVotes(int postId, [FromQuery] GetPostVotesParameters parameters)
        {
            var result = await voteFeatures.GetPostVotes(parameters);

            return new JsonResult(result);
        }
    }
}
