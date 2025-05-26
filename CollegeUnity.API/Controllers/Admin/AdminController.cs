using CollegeUnity.API.Controllers.Student;
using CollegeUnity.API.Filters;
using CollegeUnity.Contract.AdminFeatures.Communites;
using CollegeUnity.Contract.AdminFeatures.Courses;
using CollegeUnity.Contract.AdminFeatures.FeedBacks;
using CollegeUnity.Contract.AdminFeatures.ScheduleFiles;
using CollegeUnity.Contract.AdminFeatures.Staffs;
using CollegeUnity.Contract.AdminFeatures.Student;
using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Contract.SharedFeatures.ScheduleFiles;
using CollegeUnity.Core.Dtos.AdminServiceDtos;
using CollegeUnity.Core.Dtos.CommunityDtos.Create;
using CollegeUnity.Core.Dtos.CommunityDtos.Get;
using CollegeUnity.Core.Dtos.CommunityDtos.Update;
using CollegeUnity.Core.Dtos.CourseDtos;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.FeedBackDtos.Create;
using CollegeUnity.Core.Dtos.FeedBackDtos.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.ScheduleFilesDtos.Create;
using CollegeUnity.Core.Dtos.ScheduleFilesDtos.Get;
using CollegeUnity.Core.Dtos.StudentCommunityDtos.Get;
using CollegeUnity.Core.Dtos.StudentFeatures;
using CollegeUnity.Core.Dtos.StudentServiceDtos;
using CollegeUnity.Core.Dtos.SubjectDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Services.AdminFeatures.Courses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection;
using System.Text.Json;

namespace CollegeUnity.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IManageStaffFeatures _manageStaffFeatures;
        private readonly IManageCommunityFeatures _manageCommunityFeatures;
        private readonly IManageCoursesFeatures _manageCoursesFeatures;
        private readonly IManageStudentFeatures _manageStudentFeatures;
        private readonly IManageScheduleFilesFeatures _manageScheduleFilesFeatures;
        private readonly IGetScheduleFilesFeatures _getScheduleFilesFeatures;
        private readonly IManageFeedBackFeatures _manageFeedBackFeatures;

        public AdminController(IManageStaffFeatures manageStaffFeatures, IManageCommunityFeatures manageCommunityFeatures,
            IManageCoursesFeatures manageCoursesFeatures, IManageStudentFeatures manageStudentFeatures, IManageScheduleFilesFeatures manageScheduleFilesFeatures,
            IGetScheduleFilesFeatures getScheduleFilesFeatures,
            IManageFeedBackFeatures manageFeedBackFeatures
            )
        {
            _manageStaffFeatures = manageStaffFeatures;
            _manageCommunityFeatures = manageCommunityFeatures;
            _manageCoursesFeatures = manageCoursesFeatures;
            _manageStudentFeatures = manageStudentFeatures;
            _manageScheduleFilesFeatures = manageScheduleFilesFeatures;
            _getScheduleFilesFeatures = getScheduleFilesFeatures;
            _manageFeedBackFeatures = manageFeedBackFeatures;
        }

        [HttpPost("Staff/Create")]
        public async Task<IActionResult> CreateStaffAccount([FromForm] CreateStaffDto dto)
        {
            var isSuccess = await _manageStaffFeatures.CreateStaffAccount(dto);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Created(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        [HttpPost("Community/SetAdmin/{studentId}/community/{communityId}")]
        public async Task<IActionResult> SetAdmin(int studentId, int communityId, bool isSuperAdmin = true)
        {
            ResultDto isSuccess;
            if (isSuperAdmin)
                isSuccess = await _manageCommunityFeatures.SetSuperAdminForCommunity(studentId, communityId);
            else
                isSuccess = await _manageCommunityFeatures.SetAdminForCommunity(studentId, communityId);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        [HttpPost("Community/Create")]
        public async Task<IActionResult> CreateNewCommunity([FromBody] CCommunityDto dto)
        {
            var isSuccess = await _manageCommunityFeatures.CreateCommunityAsync(dto);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        [HttpPost("Schedule/Create")]
        public async Task<IActionResult> CreateScheduleFile([FromForm] CScheduleFilesDto dto)
        {
            var isSuccess = await _manageScheduleFilesFeatures.AddSchedule(dto);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Created(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        [HttpPut("User/Change/AccountStatus/{userId}")]
        public async Task<IActionResult> ChangeStaffAccountStatus(int userId, [FromBody] ChangeUserStatusDto dto)
        {
            var isSuccess = await _manageStaffFeatures.ChangeUserAccountStatus(userId, dto);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        [HttpPut("Students/Level/{isOpen}")]
        public async Task<IActionResult> UodateStateOfLevelUpgradeForStudents(bool isOpen)
        {
            await _manageStudentFeatures.OpenUpgradeStudentsLevel(isOpen);
            return new JsonResult(ApiResponse<bool?>.Success(null));
        }

        [HttpPut("Student/{studentId}/Level/{Level}")]
        public async Task<IActionResult> UodateStateOfLevelUpgradeForStudent(int studentId, Level level)
        {
            var isSuccess = await _manageStudentFeatures.OpenUpgradeStudentLevel(studentId, level);
            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        [HttpPut("Staff/Update/{staffId}")]
        public async Task<IActionResult> UpdateStaffInfo(int staffId, [FromForm] UStaffDto dto)
        {
            var isSuccess = await _manageStaffFeatures.UpdateStaffAccount(staffId, dto);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        [HttpPut("Community/Update/{communityId}")]
        public async Task<IActionResult> UpdateCommunityInfo(int communityId, [FromBody] UCommunityInfoDto dto)
        {
            var isSuccess = await _manageCommunityFeatures.EditCommunityInfo(communityId, dto);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        [HttpPut("Staff/ChangePassword/{staffId}")]
        public async Task<IActionResult> ChangeStaffPassword(int staffId, [FromBody]ChangeStaffPasswordDto dto)
        {
            var isSuccess = await _manageStaffFeatures.ChangeStaffPassword(staffId, dto);

            return new JsonResult(ApiResponse<bool?>.Success(null));

        }

        [HttpPut("Community/ChangeState/{communityId}")]
        public async Task<IActionResult> ChangeCommuntiyState(int communityId, CommunityState state)
        {
            var isSuccess = await _manageCommunityFeatures.ChangeCommunityState(communityId, state);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        [HttpPut("Community/ChangeType/{communityId}")]
        public async Task<IActionResult> ChangeCommuntiyType(int communityId, CommunityType type)
        {
            var isSuccess = await _manageCommunityFeatures.ChangeCommunityType(communityId, type);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        [HttpPut("Schedule/Update/{scheduleId}")]
        public async Task<IActionResult> UpdateScheduleFile(int scheduleId, IFormFile SchedulePicture)
        {
            var isSuccess = await _manageScheduleFilesFeatures.UpdateSchedule(scheduleId, SchedulePicture);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        [HttpPut("FeedBack/Update/{feedbackId}")]
        public async Task<IActionResult> UpdateFeedBackResponse(int feedbackId, UFeedBackResponseDto dto)
        {
            var isSuccess = await _manageFeedBackFeatures.FinalizeFeedback(feedbackId, dto);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        [HttpGet("FeedBacks")]
        public async Task<IActionResult> GetFeedBacks([FromQuery] GetFeedBackParameters parameters)
        {
            var resualts = await _manageFeedBackFeatures.GetFeedBacks(parameters);

            if (resualts != null)
            {
                return new JsonResult(ApiResponse<PagedList<GFeedBackDto>?>.Success(resualts));
            }

            return new JsonResult(ApiResponse<PagedList<GFeedBackDto>>.NotFound("No Resource yet."));
        }

        [HttpGet("Staffs")]
        public async Task<IActionResult> GetStaffBy([FromQuery] GetStaffParameters parameters)
        {
            var resualt = await _manageStaffFeatures.GetStaffs(parameters);
            return new JsonResult(ApiResponse<PagedList<GStaffByRoleDto>?>.Success(resualt));
        }

        [HttpGet("Communites")]
        public async Task<IActionResult> GetCommunites([FromQuery] GetCommunitesParameters parameters)
        {
            var communites = await _manageCommunityFeatures.GetCommunites(parameters);
            return new JsonResult(ApiResponse<PagedList<GCommunitesDto>>.Success(communites));
        }

        [HttpGet("Community/Admins")]
        public async Task<IActionResult> GetCommunityAdmins([FromQuery] GetStudentCommunityAdminsParameters parameters)
        {
            var admins = await _manageCommunityFeatures.GetAdmins(parameters);
            return new JsonResult(ApiResponse<PagedList<GCommunityAdminsDto>>.Success(admins));
        }

        [HttpGet("Students")]
        public async Task<IActionResult> GetStudents([FromQuery] GetStudentParameters parameters)
        {
            var result = await _manageStudentFeatures.GetStudents(parameters);

            return new JsonResult(ApiResponse<PagedList<GStudentDto>>.Success(result));
        }

        [HttpGet("Request/SignUp/Students")]
        public async Task<IActionResult> GetRequestSignUpStudents([FromQuery] GetStudentSignUpParameters parameters)
        {
            var result = await _manageStudentFeatures.GetStudentSignUpRequest(parameters);

            return new JsonResult(ApiResponse<PagedList<GStudentDto>>.Success(result));
        }

        [HttpGet("Schedules")]
        public async Task<IActionResult> GetScheduleFiles([FromQuery] GetScheduleFileParameters parameters)
        {
            var result = await _getScheduleFilesFeatures.GetSchedule(parameters);

            return new JsonResult(ApiResponse<PagedList<GScheduleFileDto>>.Success(result));
        }

        [HttpGet("Courses")]
        public async Task<IActionResult> GetCourseS([FromQuery] GetCoursesForAdminQS queryString)
        {
            var result = await _manageCoursesFeatures.Get(queryString);

            return new JsonResult(result);
        }

        [HttpPost("Course")]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDto dto)
        {
            var result = await _manageCoursesFeatures.Create(dto);

            return new JsonResult(result);
        }

        [HttpPut("Course/{courseId}")]
        [ValidateEntityExist(nameof(courseId))]
        public async Task<IActionResult> UpdateCourse(int courseId, [FromBody] CreateCourseDto dto)
        {
            var result = await _manageCoursesFeatures.Update(courseId, dto);

            return new JsonResult(result);
        }

        [HttpDelete("Course/{courseId}")]
        [ValidateEntityExist(nameof(courseId))]
        public async Task<IActionResult> DeleteCourse(
            int courseId,
            [FromQuery] bool ignoreRegisteredStudents = false)
        {
            var result = await _manageCoursesFeatures.Remove(courseId, ignoreRegisteredStudents);

            return new JsonResult(result);
        }

        [HttpDelete("Schedule/Delete/{scheduleId}")]
        public async Task<IActionResult> DeleteScheduleFile(int scheduleId)
        {
            var isSuccess = await _manageScheduleFilesFeatures.DeleteSchedule(scheduleId);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Created(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        [HttpDelete("Community/RemoveAdmin/{studentId}/community/{communityId}")]
        public async Task<IActionResult> RemoveAdmin(int studentId, int communityId)
        {
            var isSuccess = await _manageCommunityFeatures.RemoveAdminFromCommunites(studentId, communityId);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }
    }
}
