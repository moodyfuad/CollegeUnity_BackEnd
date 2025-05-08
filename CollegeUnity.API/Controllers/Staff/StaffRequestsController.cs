using CollegeUnity.API.Filters;
using CollegeUnity.API.Middlerware_Extentions;
using CollegeUnity.Contract.StaffFeatures.Request;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.SharedFeatures.Requests;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CollegeUnity.API.Controllers.Staff
{
    [Route("api/Staff/")]
    [ApiController]
    [Authorize(Roles =
        $"{nameof(Roles.Teacher)}," +
        $" {nameof(Roles.StudentAffairsViceDeanShip)}," +
        $" {nameof(Roles.RegistrationAdmissionEmployee)}," +
        $" {nameof(Roles.Dean)}, {nameof(Roles.HeadOfCSDepartment)}," +
        $" {nameof(Roles.HeadOfITDepartment)}")]
    public class StaffRequestsController : ControllerBase
    {
        private readonly IStaffRequestsFeatures _staffRequests;

        public StaffRequestsController(IStaffRequestsFeatures staffRequests)
        {
            _staffRequests = staffRequests;
        }

        [HttpGet("Requests")]
        public async Task<ActionResult<ApiResponse<PagedList<GetUserRequestsDto>>>>
            Get([FromQuery] GetRequestsForStaffQS queryString)
        {
            int staffId = User.GetUserId();
            return new JsonResult(await _staffRequests.GetStudentsRequestsAsync(staffId,  queryString));
        }

        [HttpPost("Request/{requestId:int}/Accept")]
        [ValidateEntityExist("requestId")]
        public async Task<ActionResult<ApiResponse<string?>>> Accept([FromRoute] int requestId)
        {
            int staffId = User.GetUserId();
            var newStatus = RequestStatus.Accepted;
            return new JsonResult(await _staffRequests.ChangeRequestStatus(staffId, requestId, newStatus));
        }

        [HttpPost("Request/{requestId:int}/Deny")]
        [ValidateEntityExist("requestId")]
        public async Task<ActionResult<ApiResponse<string?>>> Deny([FromRoute] int requestId)
        {
            int staffId = User.GetUserId();
            var newStatus = RequestStatus.Denied;
            return new JsonResult(await _staffRequests.ChangeRequestStatus(staffId, requestId, newStatus));
        }
    }
}
