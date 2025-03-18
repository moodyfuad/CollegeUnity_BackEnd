using CollegeUnity.API.Filters;
using CollegeUnity.API.Middlerware_Extentions;
using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Contract.StudentFeatures.Request;
using CollegeUnity.Contract.StudentFeatures.Subjects;
using CollegeUnity.Core.Dtos.AuthenticationDtos;
using CollegeUnity.Core.Dtos.AuthenticationServicesDtos;
using CollegeUnity.Core.Dtos.InterestedSubjectDtos;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
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
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
    public class StudentController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IStudentSubjectFeatures _studentSubjectFeatures;
        private readonly IRequestsFeature _requestsFeature;

        public StudentController(
            IServiceManager serviceManager,
            IStudentSubjectFeatures studentSubjectFeatures,
            IRequestsFeature requestsFeature)
        {
            _serviceManager = serviceManager;
            _studentSubjectFeatures = studentSubjectFeatures;
            _requestsFeature = requestsFeature;
        }

        [HttpGet("InterestedSubjects")]
        public async Task<IActionResult> GetStudentIntresetedSubject(GetInterestedSubjectParameters parameters)
        {
            int _studentId = User.GetUserId();
            var interestesSubjects = await _studentSubjectFeatures.GetStudentIntrestedSubject(parameters, _studentId);

            if (interestesSubjects != null)
            {
                return new JsonResult(ApiResponse<IEnumerable<GInterestedSubjectDto>>.Success(interestesSubjects));
            }

            return new JsonResult(ApiResponse<IEnumerable<GInterestedSubjectDto>>.NotFound("No Resource yet."));
        }


        [HttpPost("Search")]
        public async Task<IActionResult> StudentSearch([FromQuery] StudentSearchParameters searchParameters)
        {
            try
            {
                var result = await _serviceManager.StudentServices.GetStudentsAsync(searchParameters);

                if (!result.Any())
                {
                    return new JsonResult(ApiResponse<PagedList<Core.Entities.Student>>.NotFound());
                }

                if (result.Count.Equals(1))
                {
                    return new JsonResult(ApiResponse<PagedList<Core.Entities.Student>>.Success(message: $"[{result.Count}] records fetched.", data: result));
                }

                if (result.Count > 1)
                {
                    return new JsonResult(ApiResponse<PagedList<Core.Entities.Student>>.Success(message: $"[{result.Count}] records fetched.", data: result));
                }
                else{
                    return new JsonResult(ApiResponse<string>.InternalServerError(errors :["error fetching students"]));
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiResponse<string>.InternalServerError(errors: [ex.Message]));
            }
        }

        [HttpPost("Request/{staffId}")]
        [ValidateEntityExist("staffId")]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SendRequest(int staffId, SendRequestDto dto)
        {
            int _studentId = User.GetUserId();
            var response = new JsonResult(await _requestsFeature.Send(_studentId, staffId, dto));

            return response;
        }

        [HttpGet("Requests")]
        [ProducesResponseType(typeof(ApiResponse<PagedList<GetStudentRequestsDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRequests([FromQuery] GetStudentRequestsQueryString queryString)
        {
            int _studentId = User.GetUserId();
            var result = await _requestsFeature.Get(_studentId, queryString);

            var response = new JsonResult(result);

            return response;
        }
    }
}