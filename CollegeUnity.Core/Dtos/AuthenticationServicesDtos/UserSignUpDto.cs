using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.AuthenticationDtos
{
    public abstract class UserSignUpDto
    {
        
        [Required(ErrorMessage = "First Name is required")]
        public required string FirstName { get; set; }
        [Required(ErrorMessage = "Middle Name is required")]
        public required string MiddleName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public required string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid Phone number")]
        public required string Phone { get; set; }
        [Required(ErrorMessage = "Gender field is required")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }
        [Required(ErrorMessage = "Please confirm your password")]
        [Compare(nameof(Password),ErrorMessage = "Password does not match")]
        public required string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Birth Date is required")]
        public required DateOnly BirthDate { get; set; }

        [Required]
        public required AccountStatus AccountStatus { get; set; }

        public string? AccountStatusReason { get; set; }
        [Url]
        public string? ProfilePicturePath { get; set; }

        public string? VerificationCode { get; set; }

    }
}
