using CollegeUnity.API.Middlerware_Extentions;
using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Contract.SharedFeatures.Chats;
using CollegeUnity.Contract.StaffFeatures.Chats;
using CollegeUnity.Contract.StaffFeatures.Students;
using CollegeUnity.Core.Dtos.AdminServiceDtos;
using CollegeUnity.Core.Dtos.ChatDtos.Create;
using CollegeUnity.Core.Dtos.ChatDtos.Get;
using CollegeUnity.Core.Dtos.ChatDtos.Update;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.StudentFeatures;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Services;
using CollegeUnity.Services.StaffFeatures;
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
        private readonly IGetChatList _getChatList;
        private readonly IGetMyStudents _getMyStudents;
        private readonly IChatListNotificationFeatures _chatListNotificationFeatures;

        public StaffController(
            IServiceManager serviceManager, 
            IChatManagementFeatures chatManagementFeatures, 
            IGetChatList getChatList, 
            IGetMyStudents getMyStudents,
            IChatListNotificationFeatures chatListNotificationFeatures)
        {
            _adminServices = serviceManager.AdminServices;
            _chatManagementFeatures = chatManagementFeatures;
            _getChatList = getChatList;
            _getMyStudents = getMyStudents;
            _chatListNotificationFeatures = chatListNotificationFeatures;
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
        [HttpPost("Create/ChatRoom/{studentId}")]
        public async Task<IActionResult> CreateChatRoom(int studentId)
        {
            int _staffId = User.GetUserId();
            var isSuccess = await _chatManagementFeatures.CreateChatRoom(_staffId, studentId);

            if (isSuccess.success)
            {
                await _chatListNotificationFeatures.NotifyNewChat((int)isSuccess.additional, (GChatsList)isSuccess.data);
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
        [HttpPost("Update/ChatRoom/{chatRoomId}")]
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

        [Authorize(Roles = $"{nameof(Roles.Dean)}," +
            $"{nameof(Roles.StudentAffairsViceDeanShip)}," +
            $"{nameof(Roles.HeadOfCSDepartment)}," +
            $"{nameof(Roles.HeadOfITDepartment)}," +
            $"{nameof(Roles.Teacher)}," +
            $"{nameof(Roles.RegistrationAdmissionEmployee)}")]
        [HttpGet("List/Chat")]
        public async Task<IActionResult> GetChatsList([FromQuery] GetChatParameters parameters)
        {
            int _staffId = User.GetUserId();
            var list = await _getChatList.GetListOfChat(_staffId, parameters);

            if (list != null)
            {
                return new JsonResult(ApiResponse<PagedList<GChatsList>>.Success(list));
            }

            return new JsonResult(ApiResponse<PagedList<GChatsList>>.NotFound("No Resource yet."));
        }

        
        [HttpGet("Get/Teacher/Students")]
        public async Task<IActionResult> GetTeahcerStudents([FromQuery] GetMyStudentsParameters parameters)
        {
            int _teacherId = User.GetUserId();
            var list = await _getMyStudents.MyStudents(_teacherId, parameters);

            if (list != null)
            {
                return new JsonResult(ApiResponse<PagedList<GStudentDto>>.Success(list));
            }

            return new JsonResult(ApiResponse<PagedList<GStudentDto>>.NotFound("No Resource yet."));
        }

        //[HttpPost("Create")]
        //public async Task<IActionResult> CreateStaffAccount([FromBody] CreateStaffDto account)
        //{
        //    var response = await _adminServices.CreateStaffAccount(account);
        //    return Ok(response);
        //}
    }
}
