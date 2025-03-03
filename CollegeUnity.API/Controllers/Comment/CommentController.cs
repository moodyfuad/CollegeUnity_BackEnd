using CollegeUnity.API.Filters;
using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Contract.SharedFeatures.Posts.Comments;
using CollegeUnity.Core.Dtos.CommentDtos;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;
using System.Text.Json.Serialization;

namespace CollegeUnity.API.Controllers.Comment
{
    [Route("api/post/{postId}/")]
    [ApiController]
    public class CommentController(ICommentFeatures _commentFeatures) : ControllerBase
    {
        [HttpPost("comment")]
        [ValidateEntityExist("postid")]
        public async Task<IActionResult> PublishComment([FromBody] AddCommentDto dto)
        {
            var result = await _commentFeatures.AddComment(dto);

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

        [HttpGet("comments")]
        [ValidateEntityExist("postid")]

        public async Task<IActionResult> GetPublishedPostComments(int postId,[FromQuery] GetPostCommentsParameters parameters)
        {
            if (!ModelState.IsValid)

                return BadRequest("post id is required");

            PagedList<GetPostCommentDto> result = await _commentFeatures.GetPostComments(parameters);
            var meta = new
            {
                PageNumber = result.CurrentPage,
                TotalPages = result.TotalPages,
                PageSize = result.PageSize,
                HasPrevious = result.HasPrevious,
                HasNext = result.HasNext,
            };
            HttpContext.Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(meta));
            string meg = $"[{result.Count}] records fetched";
            var response = ApiResponse<PagedList<GetPostCommentDto>>.Success(result, meg);
                return new JsonResult(response);
        }

        [HttpPut("comment/update")]
        public async Task<IActionResult> UpdateComment([FromBody] EditCommentDto dto)
        {
            var result = await _commentFeatures.EditComment(dto);

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

        [HttpDelete("comment")]
        public async Task<IActionResult> DeleteComment([FromQuery, Required] int id)
        {
            var result = await _commentFeatures.DeleteComment(id);

            if (result.IsSuccess)
            {
                var response = ApiResponse<EditCommentDto>.Success(null, result.Message);
                return new JsonResult(response);
            }
            else
            {
                var response = ApiResponse<EditCommentDto>.BadRequest(
                    "Can not Delete the Comment",
                    [result.Message]);

                return new JsonResult(response);
            }
        }
    }
}
