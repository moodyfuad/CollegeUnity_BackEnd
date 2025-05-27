using CollegeUnity.API.Middlerware_Extentions;
using CollegeUnity.Contract.StaffFeatures.Account;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.SharedFeatures.Authentication;
using CollegeUnity.Core.Dtos.StaffFeatures;
using CollegeUnity.Core.Dtos.StudentFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeUnity.API.Controllers.Staff
{
    [Route("api/Staff/profile")]
    [ApiController]
    [ServiceFilter(typeof(ActiveUserAttribute))]
    public class staffProfileController : ControllerBase
    {
        private readonly IStaffProfileFeatures _staffProfileFeatures;

        public staffProfileController(IStaffProfileFeatures staffProfileFeatures)
        {
            _staffProfileFeatures = staffProfileFeatures;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<GetStaffProfileDto>>> GetStaffInfo()
        {
            int staffId = User.GetUserId();

            var result = await _staffProfileFeatures.GetInfo(staffId);

            return new JsonResult(result);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<bool>>> EditStaffInfo(UpdateUserProfileDto dto)
        {
            int staffId = User.GetUserId();

            var result = await _staffProfileFeatures.Update(staffId, dto);

            return new JsonResult(result);
        }

        [HttpPut("password")]
        public async Task<ActionResult<ApiResponse<bool>>> EditStaffPassword(UpdateUserPasswordDto dto)
        {
            int staffId = User.GetUserId();

            var result = await _staffProfileFeatures.UpdatePassword(staffId, dto);

            return new JsonResult(result);
        }
    }
}
