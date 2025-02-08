using CollegeUnity.Contract.Services_Contract;
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
        private readonly IServiceManager _serviceManager;

        public SubjectController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateNewSubject([FromForm] CreateSubjectDto dto)
        {
            var response = await _serviceManager.SubjectServices.CreateSubjectAsync(dto);

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
            var response = await _serviceManager.SubjectServices.GetAllAsync(subjectParameters);

            if (response != null)
            {
                return new JsonResult(ApiResponse<IEnumerable<SubjectDto>>.Success(data: response));
            }
            return new JsonResult(ApiResponse<SubjectDto>.NotFound("No Subjects yet."));
        }

        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> DeleteSubject(int Id)
        {
            if (await _serviceManager.SubjectServices.IsExistAsync(Id))
            {
                var response = await _serviceManager.SubjectServices.DeleteSubjectAsync(Id);

                if (response)
                {
                    return Ok(ApiResponse<bool?>.Success(data: null, "Subject deleted successfully."));
                }
                else
                {
                    return BadRequest(ApiResponse<bool?>.BadRequest("Subject can't be deleted, try again"));
                }
            }
            return NotFound(ApiResponse<bool?>.NotFound());
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateSubject([FromForm] SubjectDto dto)
        {
            if (await _serviceManager.SubjectServices.IsExistAsync(dto.Id))
            {
                var response = await _serviceManager.SubjectServices.UpdateSubjectAsync(dto);

                if (response)
                {
                    return Ok(ApiResponse<bool?>.Success(data: null, "Subject updated successfully."));
                }
                else
                {
                    return BadRequest(ApiResponse<bool?>.BadRequest("Subject can't be updated, try again"));
                }
            }

            return NotFound(ApiResponse<bool?>.NotFound());
        }
    }
}
