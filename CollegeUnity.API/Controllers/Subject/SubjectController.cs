using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Contract.StaffFeatures.Subject;
using CollegeUnity.Contract.StudentFeatures.Subjects;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.SubjectDtos;
using CollegeUnity.Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeUnity.API.Controllers.Subject
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly IManageSubjectFeatures _manageSubjectFeatures;

        public SubjectController(IManageSubjectFeatures manageSubjectFeatures)
        {
            _manageSubjectFeatures = manageSubjectFeatures;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateNewSubject([FromBody] CSubjectDto dto)
        {
            var response = await _manageSubjectFeatures.CreateSubjectAsync(dto);

            if (response.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(response.message));
        }

        [HttpPut("Update/{subjectId}")]
        public async Task<IActionResult> UpdateSubject(int subjectId, [FromBody] USubjectDto dto)
        {

            var response = await _manageSubjectFeatures.UpdateSubjectAsync(subjectId, dto);

            if (response.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null, "Subject updated successfully."));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(response.message));
        }

        [HttpPut("Assign/{teacherId}/{subjectId}")]
        public async Task<IActionResult> AssignSubjectToTeacher(int teacherId, int subjectId)
        {

            var response = await _manageSubjectFeatures.AssignSubjectToTeacher(teacherId, subjectId);

            if (response.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null, response.message));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(response.message));
        }

        [HttpGet("Subjects")]
        public async Task<IActionResult> GetSubjectsByName([FromQuery] GetSubjectParameters parameters)
        {
            var response = await _manageSubjectFeatures.GetSubjects(parameters);
            return new JsonResult(ApiResponse<PagedList<GSubjectDto>>.Success(data: response));
        }

        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> DeleteSubject(int Id)
        {
            var response = await _manageSubjectFeatures.DeleteSubjectAsync(Id);

            if (response.success)
            {
                return new JsonResult(ApiResponse<bool?>.Success(null, "Subject deleted successfully."));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(response.message));
        }
    }
}
