using CollegeUnity.API.Filters;
using CollegeUnity.API.Middlerware_Extentions;
using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Contract.SharedFeatures.Chats;
using CollegeUnity.Contract.SharedFeatures.Helpers;
using CollegeUnity.Contract.StudentFeatures.Account;
using CollegeUnity.Contract.StudentFeatures.Community;
using CollegeUnity.Contract.StudentFeatures.Request;
using CollegeUnity.Contract.StudentFeatures.Subjects;
using CollegeUnity.Core.Dtos.AuthenticationDtos;
using CollegeUnity.Core.Dtos.AuthenticationServicesDtos;
using CollegeUnity.Core.Dtos.ChatDtos.Get;
using CollegeUnity.Core.Dtos.CommunityDtos.Get;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.InterestedSubjectDtos;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.SharedFeatures.Authentication;
using CollegeUnity.Core.Dtos.SharedFeatures.Helpers;
using CollegeUnity.Core.Dtos.SharedFeatures.Requests;
using CollegeUnity.Core.Dtos.StudentFeatures;
using CollegeUnity.Core.Dtos.StudentServiceDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using EmailService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace CollegeUnity.API.Controllers.Student
{
    [Route("api/Student")]
    [ApiController]
    [Authorize(Roles = nameof(Roles.Student))]
    [ServiceFilter(typeof(ActiveUserAttribute))]
    public class StudentController : ControllerBase
    {
        private readonly IStudentSubjectFeatures _studentSubjectFeatures;
        private readonly IStudentRequestsFeatures _requestsFeature;
        private readonly IStudentCommunityFeatures _studentCommunityFeatures;
        private readonly IGetChatList _getChatList;
        private readonly ISearchUsersFeature _searchUsers;
        private readonly IStudentProfileFeatures _studentProfileFeatures;

        public StudentController(
            IStudentSubjectFeatures studentSubjectFeatures,
            IStudentRequestsFeatures requestsFeature,
            IStudentCommunityFeatures studentCommunityFeatures,
            IGetChatList getChatList,
            ISearchUsersFeature searchUsers,
            IStudentProfileFeatures studentProfileFeatures
            )
        {
            _studentSubjectFeatures = studentSubjectFeatures;
            _requestsFeature = requestsFeature;
            _studentCommunityFeatures = studentCommunityFeatures;
            _getChatList = getChatList;
            _searchUsers = searchUsers;
            _studentProfileFeatures = studentProfileFeatures;
        }

        [HttpGet("List/Chat")]
        public async Task<IActionResult> GetChatsList([FromQuery] GetChatParameters parameters)
        {
            int _studentId = User.GetUserId();
            var list = await _getChatList.GetListOfChat(_studentId, parameters);
            
            if (list != null)
            {
                return new JsonResult(ApiResponse<PagedList<GChatsList>>.Success(list));
            }

            return new JsonResult(ApiResponse<PagedList<GChatsList>>.NotFound("No Resource yet."));
        }

        [HttpGet("InterestedSubjects")]
        public async Task<IActionResult> GetStudentIntresetedSubject([FromQuery] GetInterestedSubjectParameters parameters)
        {
            int _studentId = User.GetUserId();
            var interestesSubjects = await _studentSubjectFeatures.GetStudentIntrestedSubject(parameters, _studentId);

            if (interestesSubjects != null)
            {
                return new JsonResult(ApiResponse<PagedList<GInterestedSubjectDto>>.Success(interestesSubjects));
            }

            return new JsonResult(ApiResponse<PagedList<GInterestedSubjectDto>>.NotFound("No Resource yet."));
        }

        [HttpGet("MyCommunites")]
        public async Task<IActionResult> GetStudentCommunites([FromQuery] GetStudentCommunitesParameters parameters)
        {
            int _studentId = User.GetUserId();
            var communites = await _studentCommunityFeatures.GetMyCommunites(_studentId, parameters);

            if (communites != null)
            {
                return new JsonResult(ApiResponse<PagedList<GStudentCommunitesDto>>.Success(communites));
            }

            return new JsonResult(ApiResponse<PagedList<GStudentCommunitesDto>>.NotFound("No Resource yet."));
        }

        [HttpGet("Batch/Subject/Filter")]
        public async Task<IActionResult> GetStudentSubjectsFilters([FromQuery] GetFilterBatchPostParameters parameters)
        {
            int _studentId = User.GetUserId();
            var communites = await _studentSubjectFeatures.GetStudentSubjectWithNames(_studentId, parameters);

            if (communites != null)
            {
                return new JsonResult(ApiResponse<PagedList<GStudentSubjectsDto>>.Success(communites));
            }

            return new JsonResult(ApiResponse<PagedList<GStudentCommunitesDto>>.NotFound("No Resource yet."));
        }

        [HttpGet("Communites")]
        public async Task<IActionResult> GetNotJoindCommunites([FromQuery] GetStudentCommunitesParameters parameters)
        {
            int _studentId = User.GetUserId();
            var communites = await _studentCommunityFeatures.GetNotJoinedCommunities(_studentId, parameters);

            if (communites != null)
            {
                return new JsonResult(ApiResponse<PagedList<GStudentCommunitesDto>>.Success(communites));
            }

            return new JsonResult(ApiResponse<PagedList<GStudentCommunitesDto>>.NotFound("No Resource yet."));
        }

        [HttpPost("Subject/MakeInterest/{subjectId}")]
        public async Task<IActionResult> MakeIntresetedSubject(int subjectId)
        {
            int _studentId = User.GetUserId();
            var isSuccess = await _studentSubjectFeatures.MakeSubjectInterest(_studentId, subjectId);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<ResultDto>.Success(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        [HttpDelete("Subject/MakeUnInterest/{subjectId}")]
        public async Task<IActionResult> MakeUnIntresetedSubject(int subjectId)
        {
            int _studentId = User.GetUserId();
            var isSuccess = await _studentSubjectFeatures.MakeSubjectUnInterested(_studentId, subjectId);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<ResultDto>.Success(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        [HttpPost("Community/Join/{communityId}")]
        public async Task<IActionResult> JoinToCommunity(int communityId)
        {
            int _studentId = User.GetUserId();
            var isSuccess = await _studentCommunityFeatures.JoinToCommunity(_studentId, communityId);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<ResultDto>.Success(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        [HttpDelete("Community/Leave/{communityId}")]
        public async Task<IActionResult> LeaveFromCommunity(int communityId)
        {
            int _studentId = User.GetUserId();
            var isSuccess = await _studentCommunityFeatures.LeaveFromCommunity(_studentId, communityId);

            if (isSuccess.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(isSuccess.message));
        }

        //[HttpPost("Search")]
        //public async Task<IActionResult> StudentSearch([FromQuery] StudentSearchParameters searchParameters)
        //{
        //    try
        //    {
        //        var result = await _serviceManager.StudentServices.GetStudentsAsync(searchParameters);

        //        if (!result.Any())
        //        {
        //            return new JsonResult(ApiResponse<PagedList<Core.Entities.Student>>.NotFound());
        //        }

        //        if (result.Count.Equals(1))
        //        {
        //            return new JsonResult(ApiResponse<PagedList<Core.Entities.Student>>.Success(message: $"[{result.Count}] records fetched.", data: result));
        //        }

        //        if (result.Count > 1)
        //        {
        //            return new JsonResult(ApiResponse<PagedList<Core.Entities.Student>>.Success(message: $"[{result.Count}] records fetched.", data: result));
        //        }
        //        else{
        //            return new JsonResult(ApiResponse<string>.InternalServerError(errors :["error fetching students"]));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(ApiResponse<string>.InternalServerError(errors: [ex.Message]));
        //    }
        //}

        [HttpGet("Requests")]
        public async Task<ActionResult<ApiResponse<PagedList<GetStudentRequestsDto>>>> GetRequests([FromQuery] GetStudentRequestsQueryString queryString)
        {
            int studentId = User.GetUserId();
            var result = await _requestsFeature.Get(studentId, queryString);

            var response = new JsonResult(result);

            return response;
        }

        [HttpGet("Search/Staff")]
        public async Task<ActionResult<ApiResponse<PagedList<GetStudentSearchUsersResultDto>>>> SearchStaff([FromQuery] StudentSearchUsersQS queryString)
        {
            int studentId = User.GetUserId();
            var result = await _searchUsers.SearchStaff(queryString);


            return new JsonResult(result);
        }

        [HttpPost("Request/{staffId}")]
        [ValidateEntityExist("staffId")]
        public async Task<ActionResult<ApiResponse<string?>>> SendRequest(int staffId, SendRequestDto dto)
        {
            int studentid = User.GetUserId();

            var response = new JsonResult(await _requestsFeature.Send(studentid, staffId, dto));

            return response;
        }

        [HttpGet("profile")]
        public async Task<ActionResult<ApiResponse<GetStudentProfileDto>>> GetStudentInfo()
        {
            int studentIdFromToken = User.GetUserId();

            var result = await _studentProfileFeatures.GetInfo(studentIdFromToken);

            return new JsonResult(result);
        }

        [HttpPut("profile")]
        public async Task<ActionResult<ApiResponse<bool>>> EditStudentInfo(UpdateUserProfileDto dto)
        {
            int studentId = User.GetUserId();

            var result = await _studentProfileFeatures.Update(studentId, dto);

            return new JsonResult(result);
        }

        [HttpPut("profile/password")]
        public async Task<ActionResult<ApiResponse<bool>>> EditStudentPassword(UpdateUserPasswordDto dto)
        {
            int studentId = User.GetUserId();

            var result = await _studentProfileFeatures.UpdatePassword(studentId, dto);

            return new JsonResult(result);
        }
    }
}