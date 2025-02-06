using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Core.Dtos.PostDtos.Create;
using CollegeUnity.Core.Dtos.ResponseDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeUnity.API.Controllers.Post
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostServices _postServices;
        private readonly IStaffServices _staffServices;

        public PostController(IServiceManager serviceManager)
        {
            _postServices = serviceManager.PostServices;
            _staffServices = serviceManager.StaffServices;
        }

        [HttpPost("Public/Create")]
        public async Task<IActionResult> CreatePublicPost([FromForm] CPublicPostDto dto)
        {
            bool isExist = await _staffServices.IsExistAsync(dto.StaffId);
            if (isExist)
            {
                await _postServices.CreatePublicPostAsync(dto);
                return new JsonResult(ApiResponse<bool?>.Created(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest("Something wrong, try again."));
        }
    }
}
