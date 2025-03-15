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
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IStudentSubjectFeatures _studentSubjectFeatures;
        private readonly IRequestsFeature _sendRequestFeature;

        public StudentController(IServiceManager serviceManager,
            IStudentSubjectFeatures studentSubjectFeatures,
            IRequestsFeature sendRequestFeatures)
        {
            _serviceManager = serviceManager;
            _studentSubjectFeatures = studentSubjectFeatures;
            _sendRequestFeature = sendRequestFeatures;
        }

        [HttpGet("InterestedSubjects")]
        public async Task<IActionResult> GetStudentIntresetedSubject(GetInterestedSubjectParameters parameters)
        {
            var studentId = User.GetUserId();
            var interestesSubjects = await _studentSubjectFeatures.GetStudentIntrestedSubject(parameters, studentId);

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

                Response.AddPagination(result.CurrentPage, result.TotalPages, result.PageSize, result.HasPrevious, result.HasNext);

                if (!result.Any())
                    return Ok(ApiResponse<List<Core.Entities.Student>>.NotFound());

                if (result.Count.Equals(1))
                    return Ok(ApiResponse<Core.Entities.Student>.Success(data: result.FirstOrDefault()!));

                if (result.Count > 1)
                    return Ok(ApiResponse<PagedList<Core.Entities.Student>>.Success(message: $"[{result.Count}] records fetched.",data: result));
                else 
                    return Ok(ApiResponse<string>.InternalServerError(errors :["error fetching students"]));

            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<string>.InternalServerError(errors: [ex.Message]));
            }
        }

        [HttpPost("Request/{staffId}")]
        [ValidateEntityExist("staffId")]
        public async Task<IActionResult> SendRequest(int staffId, SendRequestDto dto)
        {
            int studentid = User.GetUserId();

            var response = new JsonResult(await _sendRequestFeature.Send(studentid, staffId, dto));

            return response;
        }

        [HttpGet("Requests")]
        public async Task<IActionResult> GetRequests()
        {
            int studentid = User.GetUserId();

            var response = new JsonResult(await _sendRequestFeature.Get(studentid));

            return response;
        }
    }
}