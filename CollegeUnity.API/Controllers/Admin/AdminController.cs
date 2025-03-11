using CollegeUnity.API.Controllers.Student;
using CollegeUnity.Contract.AdminFeatures.Communites;
using CollegeUnity.Contract.AdminFeatures.Staffs;
using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Core.Dtos.AdminServiceDtos;
using CollegeUnity.Core.Dtos.CommunityDtos.Create;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.StudentServiceDtos;
using CollegeUnity.Core.Dtos.SubjectDtos;
using CollegeUnity.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.ComponentModel.DataAnnotations;
using System.Net;
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

        //[HttpGet("Staff")]
        //public async Task<IActionResult> Get([Required][FromQuery(Name = "Name")] string name, StaffParameters staffParameters) 
        //{
        //    var response = await _serviceManager.AdminServices.SearchStaffBy(name,staffParameters);
        //    HttpContext.Response.StatusCode = response.StatusCode;
            
        //    return new JsonResult(response); 
        //}

        //[HttpGet("SearchStudents")]
        //public async Task<IActionResult> GetStudent([FromQuery] StudentSearchParameters parameters) 
        //{
        //    //return await new StudentController(serviceManager: _serviceManager);
        //   /* var response = await _serviceManager.StudentServices.GetStudentsAsync(parameters);
        //    HttpContext.Response.StatusCode = response.StatusCode;*/

        //    return Ok("Not implemented"); 
        //}

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

        [HttpPut("Staff/Update/{staffId}")]
        public async Task<IActionResult> UpdateStaffInfo(int staffId, UStaffDto dto)
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
        public async Task<IActionResult> UpdateStaffInfo(int staffId, [FromForm]ChangeStaffPasswordDto dto)
        {
            var isSuccess = await _manageStaffFeatures.ChangeStaffPassword(staffId, dto);

            return new JsonResult(ApiResponse<bool?>.Success(null, pageNumber: null, pageSize: null));

        }

        [HttpPost("Community/Create")]
        public async Task<IActionResult> CreateStaffAccount([FromForm] CCommunityDto dto)
        {
            var isSuccess = await _manageCommunityFeatures.CreateCommunityAsync(dto);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null, pageNumber: null, pageSize: null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        [HttpPost("Community/SetSuperAdmin")]
        public async Task<IActionResult> CreateStaffAccount([FromForm] int studentId, int communityId)
        {
            var isSuccess = await _manageCommunityFeatures.SetSuperAdminForCommunity(studentId, communityId);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null, pageNumber: null, pageSize: null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }
    }
}
