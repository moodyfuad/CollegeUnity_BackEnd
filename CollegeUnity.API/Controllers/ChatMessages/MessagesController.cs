using CollegeUnity.API.Middlerware_Extentions;
using CollegeUnity.Contract.SharedFeatures.Messages;
using CollegeUnity.Contract.StudentFeatures.Community;
using CollegeUnity.Core.Dtos.ChatDtos.Get;
using CollegeUnity.Core.Dtos.MessagesDto.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeUnity.API.Controllers.ChatMessages
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ActiveUserAttribute))]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageFeatures _messageFeatures;
        private readonly IStudentCommunityFeatures _studentCommunityFeatures;

        public MessagesController(IMessageFeatures messageFeatures, IStudentCommunityFeatures studentCommunityFeatures)
        {
            _messageFeatures = messageFeatures;
            _studentCommunityFeatures = studentCommunityFeatures;
        }

        [HttpGet("Chat/{chatId}")]
        public async Task<IActionResult> GetMessages(int chatId, [FromQuery] GetChatMessagesParameters parameters)
        {
            int userId = User.GetUserId();
            var results = await _messageFeatures.GetChatMessages(chatId, userId, parameters);

            if (results != null)
            {
                return new JsonResult(ApiResponse<PagedList<ChatMessage>>.Success(results));
            }

            return new JsonResult(ApiResponse<PagedList<GChatsList>>.NotFound("No Resource yet."));
        }

        [HttpGet("Community/{communityId}")]
        public async Task<IActionResult> GetCommunityMessages(int communityId, [FromQuery] GetCommunityMessagesParameters parameters)
        {
            int userId = User.GetUserId();
            var results = await _studentCommunityFeatures.GetCommunityMessages(userId, communityId, parameters);

            if (results != null)
            {
                return new JsonResult(ApiResponse<PagedList<GCommunityMessagesDto>>.Success(results));
            }

            return new JsonResult(ApiResponse<PagedList<GCommunityMessagesDto>>.NotFound("No Resource yet."));
        }

    }
}
