using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.AdminServiceDtos
{
    public class CreateStaffDto
    {
       
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string MiddleName { get; set; }
        [Required]
        public required string LastName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        [Phone]
        public required string Phone { get; set; }

        [Required]
        public required string Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        public required string ConfirmPassword { get; set; }

        public required DateOnly BirthDate { get; set; }

        [Required]
        public Gender Gender { get; set; }
        [Required]
        public required AccountStatus AccountStatus { get; set; }

        [Required]
        public required ICollection<Roles> Roles { get; set; }

        [Required]
        public required EducationDegree EducationDegree { get; set; }

    }
}
