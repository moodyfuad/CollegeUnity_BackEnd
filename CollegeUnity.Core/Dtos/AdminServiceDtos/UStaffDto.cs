using CollegeUnity.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.AdminServiceDtos
{
    public class UStaffDto
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }        
        public string? LastName { get; set; }
        public EducationDegree? EducationDegree { get; set; }
        
        public IFormFile? ProfilePicturePath { get; set; }        
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateOnly? BirthDate { get; set; }
        public Gender? Gender { get; set; }
        public ICollection<Roles>? Roles { get; set; }
    }
}
