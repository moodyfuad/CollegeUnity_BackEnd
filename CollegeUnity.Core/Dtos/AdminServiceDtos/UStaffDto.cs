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
        [Required(ErrorMessage = "First Name is required")]
        public required string FirstName { get; set; }
        
        [Required(ErrorMessage = "Middle Name is required")]
        public required string MiddleName { get; set; }
        
        [Required(ErrorMessage = "Last Name is required")]
        public required string LastName { get; set; }
        
        [Required(ErrorMessage = "Education Degree is required")]
        public required EducationDegree EducationDegree { get; set; }
        
        public IFormFile? ProfilePicturePath { get; set; }
        
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public required string Email { get; set; }
        
        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid Phone number")]
        public required string Phone { get; set; }
        
        [Required(ErrorMessage = "Birth Date is required")]
        public required DateOnly BirthDate { get; set; }

        [Required(ErrorMessage = "Gender field is required")]
        public required Gender Gender { get; set; }

        [Required(ErrorMessage = "Roles Field is required")]
        public required ICollection<Roles> Roles { get; set; }

        [Required]
        public required AccountStatus AccountStatus { get; set; }
    }
}
