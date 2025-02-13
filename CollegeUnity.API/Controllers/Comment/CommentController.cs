using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Core.Dtos.CommentDtos;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Text.Json.Serialization;

namespace CollegeUnity.API.Controllers.Comment
{
    [Route("api/")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CommentController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("post/comment/Create")]
        public async Task<IActionResult> PublishComment([FromBody] AddCommentDto dto)
        {
            var result = await _serviceManager.CommentService.AddComment(dto);

            if (result.IsSuccess)
            {
                var response = ApiResponse<AddCommentResultDto>.Created(null, result.Message);
                return new JsonResult(response);
            }
            else
            {
            var response = ApiResponse<AddCommentResultDto>.BadRequest(result.Message);
            return new JsonResult(response);
            }
        }

        [HttpGet("post/comments")]
        public async Task<IActionResult> GetPostComments([FromQuery] GetPostCommentsParameters parameters)
        {
            if (!ModelState.IsValid)

                return BadRequest("post id is required");

            PagedList<GetPostCommentDto> result = await _serviceManager.CommentService.GetPostComments(parameters);
            var meta = new
            {
                PageNumber = result.CurrentPage,
                TotalPages = result.TotalPages,
                PageSize = result.PageSize,
                HasPrevious = result.HasPrevious,
                HasNext = result.HasNext,
            };
            HttpContext.Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(meta));
            var response = ApiResponse<PagedList<GetPostCommentDto>>.Success(result);
                return new JsonResult(response);
        }

        [HttpPut("post/comment/update")]
        public async Task<IActionResult> UpdateComment([FromBody] EditCommentDto dto)
        {
            var result = await _serviceManager.CommentService.EditComment(dto);

            if (result.IsSuccess.HasValue && result.IsSuccess.Value)
            {
                var response = ApiResponse<EditCommentDto>.Success(null, "Comment Updated");
                return new JsonResult(response);
            }
            else
            {
                var response = ApiResponse<EditCommentDto>.BadRequest("Can not Update the Comment",[result.Message]);
                return new JsonResult(response);
            }
        }

    }
}
