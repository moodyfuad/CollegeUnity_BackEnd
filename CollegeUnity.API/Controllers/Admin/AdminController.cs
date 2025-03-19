using CollegeUnity.API.Controllers.Student;
using CollegeUnity.Contract.AdminFeatures.Communites;
using CollegeUnity.Contract.AdminFeatures.Staffs;
using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Core.Dtos.AdminServiceDtos;
using CollegeUnity.Core.Dtos.CommunityDtos.Create;
using CollegeUnity.Core.Dtos.CommunityDtos.Get;
using CollegeUnity.Core.Dtos.CommunityDtos.Update;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.StudentCommunityDtos.Get;
using CollegeUnity.Core.Dtos.StudentServiceDtos;
using CollegeUnity.Core.Dtos.SubjectDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
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
        private readonly IServiceManager _serviceManager;
        private readonly IManageStaffFeatures _manageStaffFeatures;
        private readonly IManageCommunityFeatures _manageCommunityFeatures;

        public AdminController(IServiceManager serviceManager, IManageStaffFeatures manageStaffFeatures, IManageCommunityFeatures manageCommunityFeatures)
        {
            _serviceManager = serviceManager;
            _manageStaffFeatures = manageStaffFeatures;
            _manageCommunityFeatures = manageCommunityFeatures;
        }

        [HttpPost("Staff/Create")]
        public async Task<IActionResult> CreateStaffAccount([FromForm] CreateStaffDto dto)
        {
            var isSuccess = await _manageStaffFeatures.CreateStaffAccount(dto);

            if (isSuccess)
            {
                return new JsonResult(ApiResponse<bool?>.Created(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest("There is staff with the same email or phone, try again."));
        }

        [HttpPost("Community/Update/{communityId}")]
        public async Task<IActionResult> UpdateCommunityInfo(int communityId, [FromBody] UCommunityInfoDto dto)
        {
            var isSuccess = await _manageCommunityFeatures.EditCommunityInfo(communityId, dto);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        [HttpPost("Community/RemoveAdmin/{studentId}")]
        public async Task<IActionResult> ReemoveAdmin(int studentId, [FromBody] int communityId)
        {
            var isSuccess = await _manageCommunityFeatures.RemoveAdminFromCommunites(studentId, communityId);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        [HttpPost("Community/SetAdmin/{studentId}")]
        public async Task<IActionResult> SetAdmin(int studentId, [FromBody] int communityId)
        {
            var isSuccess = await _manageCommunityFeatures.SetAdminForCommunity(studentId, communityId);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        [HttpPost("Community/SetSuperAdmin/{studentId}")]
        public async Task<IActionResult> SetSuperAdmin(int studentId, [FromBody] int communityId)
        {
            var isSuccess = await _manageCommunityFeatures.SetSuperAdminForCommunity(studentId, communityId);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        [HttpPost("Staff/Change/AccountStatus{staffId}")]
        public async Task<IActionResult> ChangeStaffAccountStatus(int staffId, [FromBody] ChangeStaffStatusDto dto)
        {
            var isSuccess = await _manageStaffFeatures.ChangeStaffAccountStatus(staffId, dto);

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

        [HttpPut("Staff/Update/{staffId}")]
        public async Task<IActionResult> UpdateStaffInfo(int staffId, [FromForm] UStaffDto dto)
        {
            var isSuccess = await _manageStaffFeatures.UpdateStaffAccount(staffId, dto);

            if (isSuccess)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest("There is staff with the same email or phone, try again."));
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

        [HttpGet("Staffs")]
        public async Task<IActionResult> GetStaffBy([FromQuery] GetStaffParameters parameters)
        {
            if (!string.IsNullOrEmpty(parameters.FullName) && parameters.Role == null)
            {
                var resualt = await _manageStaffFeatures.GetStaffByFullName(parameters);
                return new JsonResult(ApiResponse<PagedList<GStaffByRoleDto>?>.Success(resualt));
            }

            else if (string.IsNullOrEmpty(parameters.FullName) && parameters.Role != null)
            {
                var resualt = await _manageStaffFeatures.GetStaffByRole(parameters);
                return new JsonResult(ApiResponse<PagedList<GStaffDto>?>.Success(resualt));
            }

            else
            {
                var resualt = await _manageStaffFeatures.GetAllStaff(parameters);
                return new JsonResult(ApiResponse<PagedList<GStaffByRoleDto>?>.Success(resualt));
            }
        }

        [HttpGet("Communites")]
        public async Task<IActionResult> GetCommunites([FromQuery] GetCommunitesParameters parameters)
        {
            if (parameters.CommunityState != null)
            {
                var communites = await _manageCommunityFeatures.GetCommunitesByState(parameters);
                return new JsonResult(ApiResponse<PagedList<GCommunitesDto>>.Success(communites));
            }

            else if (parameters.CommunityType != null)
            {
                var communites = await _manageCommunityFeatures.GetCommunitesByType(parameters);
                return new JsonResult(ApiResponse<PagedList<GCommunitesDto>>.Success(communites));
            }

            else
            {
                var communites = await _manageCommunityFeatures.GetCommunites(parameters);
                return new JsonResult(ApiResponse<PagedList<GCommunitesDto>>.Success(communites));
            }
        }

        [HttpGet("Community/Admins")]
        public async Task<IActionResult> GetCommunityAdmins([FromQuery]GetStudentCommunityAdminsParameters parameters)
        {
            var admins = await _manageCommunityFeatures.GetAdmins(parameters);
            return new JsonResult(ApiResponse<PagedList<GCommunityAdminsDto>>.Success(admins));
        }
    }
}
