using CollegeUnity.API.Filters;
using CollegeUnity.API.Middlerware_Extentions;
using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Contract.SharedFeatures.Posts.Comments;
using CollegeUnity.Core.Dtos.CommentDtos;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;
using System.Text.Json.Serialization;

namespace CollegeUnity.API.Controllers.Comment
{
    [Route("api/post/{postId:int}/")]
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(ActiveUserAttribute))]
    public class CommentController(ICommentFeatures _commentFeatures) : ControllerBase
    {
        [HttpPost("comment")]
        [ValidateEntityExist("postid")]

        // TODO : edit the response to be just ApiResponse<bool>
        public async Task<ActionResult<ApiResponse<AddCommentResultDto>>> PublishComment([FromRoute] int postId, [FromBody] AddCommentDto dto)
        {
            int userId = User.GetUserId();

            var result = await _commentFeatures.AddComment(userId ,postId, dto);

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
        public async Task<ActionResult<ApiResponse<PagedList<GetPostCommentDto>>>>
            GetPublishedPostComments([FromRoute] int postId, [FromQuery] GetPostCommentsParameters parameters)
        {
            PagedList<GetPostCommentDto> result = await _commentFeatures.GetPostComments(postId, parameters);

            string meg = $"[{result.Count}] records fetched";
            var response = ApiResponse<PagedList<GetPostCommentDto>>.Success(result, meg);
                return new JsonResult(response);
        }

        [ValidateEntityExist("postid")]
        [ValidateEntityExist("commentid")]
        [HttpPut("comment/{commentId}")]
        public async Task<ActionResult<ApiResponse<EditCommentDto>>> UpdateComment(int commentId, [FromBody] EditCommentDto dto)
        {
            int userId = User.GetUserId();
            dto.id = commentId;

            var result = await _commentFeatures.EditComment(userId, dto);

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

        [HttpDelete("comment/{commentId}")]
        [ValidateEntityExist("commentid")]
        [ValidateEntityExist("postid")]
        public async Task<ActionResult<ApiResponse<EditCommentDto>>> DeleteComment(int commentId)
        {
            int userId = User.GetUserId();

            var result = await _commentFeatures.DeleteComment(userId, commentId);

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
