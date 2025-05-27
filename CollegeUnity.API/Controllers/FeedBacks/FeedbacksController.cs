using CollegeUnity.Contract.SharedFeatures.Feedbacks;
using CollegeUnity.Core.Dtos.FeedBackDtos.Create;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeUnity.API.Controllers.FeedBacks
{
    [Authorize(Roles = $"{nameof(Roles.Dean)}," +
    $"{nameof(Roles.StudentAffairsViceDeanShip)}," +
    $"{nameof(Roles.HeadOfCSDepartment)}," +
    $"{nameof(Roles.HeadOfITDepartment)}," +
    $"{nameof(Roles.Teacher)}," +
    $"{nameof(Roles.Student)}," +
    $"{nameof(Roles.RegistrationAdmissionEmployee)}")]
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ActiveUserAttribute))]
    public class FeedbacksController : ControllerBase
    {
        private readonly ISendFeedBackFeatures _sendFeedBackFeatures;

        public FeedbacksController(ISendFeedBackFeatures sendFeedBackFeatures)
        {
            _sendFeedBackFeatures = sendFeedBackFeatures;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> SendFeedback(int userId, CFeedBackResponseDto dto)
        {
            var result = await _sendFeedBackFeatures.SendFeedBack(userId, dto);
            if (result.success)
            {
                return new JsonResult(ApiResponse<bool?>.Created(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(result.message));
        }
    }
}
