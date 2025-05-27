using CollegeUnity.API.Middlerware_Extentions;
using CollegeUnity.Contract.StudentFeatures.Community;
using CollegeUnity.Core.Dtos.CommunityDtos.Get;
using CollegeUnity.Core.Dtos.CommunityDtos.Update;
using CollegeUnity.Core.Dtos.PostDtos.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeUnity.API.Controllers.Communites
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ActiveUserAttribute))]
    public class CommunityController : ControllerBase
    {
        private readonly IStudentCommunityFeatures _studentCommunityFeatures;

        public CommunityController(IStudentCommunityFeatures studentCommunityFeatures)
        {
            _studentCommunityFeatures = studentCommunityFeatures;
        }

        [HttpGet("Students/{communityId}")]
        public async Task<IActionResult> GetStudentsOfCommunity(int communityId, [FromQuery] CommunityStudentsParameters parameters)
        {
            var posts = await _studentCommunityFeatures.GetStudentsOfCommunity(communityId, parameters);
            if (posts.Count() > 0)
            {
                return new JsonResult(ApiResponse<PagedList<GCommunityStudentsDto>>.Success(data: posts));
            }

            return new JsonResult(ApiResponse<PagedList<GCommunityStudentsDto>?>.NotFound("No Posts yet."));
        }

        [HttpPut("Update/{communityId}")]
        public async Task<IActionResult> UpdateCommunityInfo(int communityId, UCommunityInfoByStudentDto dto)
        {
            int studentId = User.GetUserId();
            var result = await _studentCommunityFeatures.EditCommunityInfoByStudent(studentId, communityId, dto);
            if (result.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(result.message));
        }

        [HttpPut("Update/Student/Role/{communityId}")]
        public async Task<IActionResult> UpdateStudentRole(int communityId, UStudentRoleInCommunityDto dto)
        {
            int studentId = User.GetUserId();
            var result = await _studentCommunityFeatures.UpdateStudentRole(studentId, dto);
            if (result.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(result.message));
        }
    }
}
