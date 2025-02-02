using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Core.Dtos.AdminServiceDtos;
using CollegeUnity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;

namespace CollegeUnity.API.Controllers.Staff
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IAdminServices _adminServices;

        public StaffController(IServiceManager serviceManager)
        {
            _adminServices = serviceManager.AdminServices;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _adminServices.GetAllStaff();
            return new JsonResult(response);
        }

        [HttpGet("{FirstName}")]
        public async Task<IActionResult> Get(string FirstName)
        {
            var response = await _adminServices.SearchStaffBy(FirstName);
            return new JsonResult(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateStaffAccount([FromBody] CreateStaffDto account)
        {
            var response = await _adminServices.CreateStaffAccount(account);
            return Ok(response);
        }
    }
}
