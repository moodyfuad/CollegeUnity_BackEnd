using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Contract.StaffFeatures.Subject;
using CollegeUnity.Contract.StudentFeatures.Subjects;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.SubjectDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeUnity.API.Controllers.Subject
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly IManageSubjectFeatures _manageSubjectFeatures;
        private readonly IStudentSubjectFeatures _studentSubjectFeatures;

        public SubjectController(IStudentSubjectFeatures studentSubjectFeatures, IManageSubjectFeatures manageSubjectFeatures)
        {
            _manageSubjectFeatures = manageSubjectFeatures;
            _studentSubjectFeatures = studentSubjectFeatures;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateNewSubject([FromForm] CSubjectDto dto)
        {
            var response = await _manageSubjectFeatures.CreateSubjectAsync(dto);

            if (response)
            {
                return new JsonResult(ApiResponse<bool?>.Created(data: null));
            }
            else
            {
                return new JsonResult(ApiResponse<bool?>.BadRequest("Subject already exists."));
            }
        }

        [HttpGet("Subjects")]
        public async Task<IActionResult> GetSubjects([FromQuery] SubjectParameters subjectParameters)
        {
            var response = await _manageSubjectFeatures.GetAllAsync(subjectParameters);

            if (response != null)
            {
                return new JsonResult(ApiResponse<IEnumerable<GSubjectDto>>.Success(data: response));
            }
            return new JsonResult(ApiResponse<GSubjectDto>.NotFound("No Subjects yet."));
        }

        [HttpGet("ByName")]
        public async Task<IActionResult> GetSubjectsByName([FromQuery] GetSubjectByNameParameters parameters)
        {
            var response = await _manageSubjectFeatures.GetSubjectsByName(parameters);

            if (response != null)
            {
                return new JsonResult(ApiResponse<IEnumerable<GSubjectDto>>.Success(data: response));
            }
            return new JsonResult(ApiResponse<GSubjectDto>.NotFound("No Subjects with this Name yet."));
        }

        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> DeleteSubject(int Id)
        {
            var response = await _manageSubjectFeatures.DeleteSubjectAsync(Id);

            if (response)
            {
                return new JsonResult(ApiResponse<bool?>.Success(data: null, "Subject deleted successfully."));
            }
            else
            {
                return new JsonResult(ApiResponse<bool?>.NotFound("No Subject, try again."));
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateSubject([FromForm] USubjectDto dto)
        {
            
            var response = await _manageSubjectFeatures.UpdateSubjectAsync(dto);

            if (response)
            {
                return new JsonResult(ApiResponse<bool?>.Success(data: null, "Subject updated successfully."));
            }
            else
            {
                return new JsonResult(ApiResponse<bool?>.BadRequest("nO Subject, try again"));
            }
        }
    }
}
