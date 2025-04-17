using CollegeUnity.API.Middlerware_Extentions;
using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Contract.StaffFeatures.Chats;
using CollegeUnity.Core.Dtos.AdminServiceDtos;
using CollegeUnity.Core.Dtos.ChatDtos.Create;
using CollegeUnity.Core.Dtos.ChatDtos.Update;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Enums;
using CollegeUnity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;

namespace CollegeUnity.API.Controllers.Staff
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IAdminServices _adminServices;
        private readonly IChatManagementFeatures _chatManagementFeatures;

        public StaffController(IServiceManager serviceManager, IChatManagementFeatures chatManagementFeatures)
        {
            _adminServices = serviceManager.AdminServices;
            _chatManagementFeatures = chatManagementFeatures;
        }

        //[HttpGet]
        //public async Task<IActionResult> Get([FromQuery] StaffParameters staffParameters)
        //{
        //    var response = await _adminServices.GetAllStaff(staffParameters);
        //    return new JsonResult(response);
        //}

        [HttpGet("{FirstName}")]
        public async Task<IActionResult> Get(string FirstName, StaffParameters staffParameters)
        {
            var response = await _adminServices.SearchStaffBy(FirstName, staffParameters);
            return new JsonResult(response);
        }

        [Authorize(Roles = $"{nameof(Roles.Dean)}," +
            $"{nameof(Roles.StudentAffairsViceDeanShip)}," +
            $"{nameof(Roles.HeadOfCSDepartment)}," +
            $"{nameof(Roles.HeadOfITDepartment)}," +
            $"{nameof(Roles.Teacher)}," +
            $"{nameof(Roles.RegistrationAdmissionEmployee)}")]
        [HttpPost("Create/ChatRoom")]
        public async Task<IActionResult> CreateChatRoom(CChatDto dto)
        {
            var isSuccess = await _chatManagementFeatures.CreateChatRoom(dto);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Created(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        [Authorize(Roles = $"{nameof(Roles.Dean)}," +
            $"{nameof(Roles.StudentAffairsViceDeanShip)}," +
            $"{nameof(Roles.HeadOfCSDepartment)}," +
            $"{nameof(Roles.HeadOfITDepartment)}," +
            $"{nameof(Roles.Teacher)}," +
            $"{nameof(Roles.RegistrationAdmissionEmployee)}")]
        [HttpPost("Update/ChatRoom/{ChatRoomId}")]
        public async Task<IActionResult> UpdateChatRoom(int chatRoomId, UChatDto dto)
        {
            var staffId = User.GetUserId();
            var isSuccess = await _chatManagementFeatures.ManageStatusChatRoom(chatRoomId, staffId, dto);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Created(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        //[HttpPost("Create")]
        //public async Task<IActionResult> CreateStaffAccount([FromBody] CreateStaffDto account)
        //{
        //    var response = await _adminServices.CreateStaffAccount(account);
        //    return Ok(response);
        //}
    }
}
