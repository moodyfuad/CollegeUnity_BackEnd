using CollegeUnity.API.Controllers.Student;
using CollegeUnity.Contract.AdminFeatures.Communites;
using CollegeUnity.Contract.AdminFeatures.Staffs;
using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Core.Dtos.AdminServiceDtos;
using CollegeUnity.Core.Dtos.CommunityDtos.Create;
using CollegeUnity.Core.Dtos.CommunityDtos.Update;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.StudentCommunityDtos.Get;
using CollegeUnity.Core.Dtos.StudentServiceDtos;
using CollegeUnity.Core.Dtos.SubjectDtos;
using CollegeUnity.Core.Entities;
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

        [HttpPut("Staff/Update")]
        public async Task<IActionResult> UpdateStaffInfo([FromHeader] int staffId, [FromForm] UStaffDto dto)
        {
            var isSuccess = await _manageStaffFeatures.UpdateStaffAccount(staffId, dto);

            if (isSuccess)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest("There is staff with the same email or phone, try again."));
        }

        [HttpGet("Staffs")]
        public async Task<IActionResult> GetStaffBy([FromQuery] GetStaffParameters parameters)
        {
            if (!string.IsNullOrEmpty(parameters.FullName) && parameters.Role == null)
            {
                var resualt = await _manageStaffFeatures.GetStaffByFullName(parameters);
                return new JsonResult(ApiResponse<IEnumerable<GStaffByRoleDto>?>.Success(resualt, pageNumber: parameters.PageNumber, pageSize:parameters.PageSize));
            }

            else if (string.IsNullOrEmpty(parameters.FullName) && parameters.Role != null)
            {
                var resualt = await _manageStaffFeatures.GetStaffByRole(parameters);
                return new JsonResult(ApiResponse<IEnumerable<GStaffDto>?>.Success(resualt, pageNumber: parameters.PageNumber, pageSize: parameters.PageSize));
            }

            else
            {
                var resualt = await _manageStaffFeatures.GetAllStaff(parameters);
                return new JsonResult(ApiResponse<IEnumerable<GStaffDto>?>.Success(resualt, pageNumber: parameters.PageNumber, pageSize: parameters.PageSize));
            }
        }

        [HttpPut("Staff/ChangePassword/{staffId}")]
        public async Task<IActionResult> ChangeStaffPassword(int staffId, [FromForm]ChangeStaffPasswordDto dto)
        {
            var isSuccess = await _manageStaffFeatures.ChangeStaffPassword(staffId, dto);

            return new JsonResult(ApiResponse<bool?>.Success(null, pageNumber: null, pageSize: null));

        }

        [HttpPost("Community/Create")]
        public async Task<IActionResult> CreateNewCommunity([FromForm] CCommunityDto dto)
        {
            var isSuccess = await _manageCommunityFeatures.CreateCommunityAsync(dto);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null, pageNumber: null, pageSize: null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        [HttpPost("Staff/Change/AccountStatus{staffId}")]
        public async Task<IActionResult> ChangeStaffAccountStatus(int staffId, [FromForm]ChangeStaffStatusDto dto)
        {
            var isSuccess = await _manageStaffFeatures.ChangeStaffAccountStatus(staffId, dto);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null, pageNumber: null, pageSize: null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        [HttpPost("Community/SetSuperAdmin/{studentId}")]
        public async Task<IActionResult> SetSuperAdmin(int studentId, [FromForm] int communityId)
        {
            var isSuccess = await _manageCommunityFeatures.SetSuperAdminForCommunity(studentId, communityId);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null, pageNumber: null, pageSize: null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        [HttpPost("Community/SetAdmin/{studentId}")]
        public async Task<IActionResult> SetAdmin(int studentId, [FromForm] int communityId)
        {
            var isSuccess = await _manageCommunityFeatures.SetAdminForCommunity(studentId, communityId);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null, pageNumber: null, pageSize: null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        [HttpGet("Community/Admins")]
        public async Task<IActionResult> GetCommunityAdmins([FromQuery]GetStudentCommunityAdminsParameters parameters)
        {
            var admins = await _manageCommunityFeatures.GetAdmins(parameters);
            return new JsonResult(ApiResponse<IEnumerable<GCommunityAdmins>>.Success(admins, pageNumber: null, pageSize: null));
        }


        [HttpPost("Community/Update/{communityId}")]
        public async Task<IActionResult> UpdateCommunityInfo(int communityId, [FromForm] UCommunityInfoDto dto)
        {
            var isSuccess = await _manageCommunityFeatures.EditCommunityInfo(communityId, dto);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null, pageNumber: null, pageSize: null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }
    }
}
