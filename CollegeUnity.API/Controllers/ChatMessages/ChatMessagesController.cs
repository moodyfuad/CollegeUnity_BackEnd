using CollegeUnity.API.Middlerware_Extentions;
using CollegeUnity.Contract.SharedFeatures.Messages;
using CollegeUnity.Core.Dtos.ChatDtos.Get;
using CollegeUnity.Core.Dtos.MessagesDto.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeUnity.API.Controllers.ChatMessages
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessagesController : ControllerBase
    {
        private readonly IMessageFeatures _messageFeatures;

        public ChatMessagesController(IMessageFeatures messageFeatures)
        {
            _messageFeatures = messageFeatures;
        }

        [HttpGet("Chat/Messages/{chatId}")]
        public async Task<IActionResult> GetMessages(int chatId, [FromQuery] GetChatMessagesParameters parameters)
        {
            int userId = User.GetUserId();
            var results = await _messageFeatures.GetChatMessages(chatId, userId, parameters);

            if (results != null)
            {
                return new JsonResult(ApiResponse<PagedList<GChatMessagesDto>>.Success(results));
            }

            return new JsonResult(ApiResponse<PagedList<GChatsList>>.NotFound("No Resource yet."));
        }

    }
}
