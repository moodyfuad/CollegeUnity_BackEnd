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
    public class UserClaimsDto
    {
        public int Id { get; set; }
        [Url]
        public string? ProfilePicturePath { get; set; }

        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string MiddleName { get; set; }
        [Required]
        public required string LastName { get; set; }

        public string FullName
        {
            get { return $"{FirstName} {MiddleName} {LastName}"; }
        }

        [Required]
        public Gender Gender { get; set; }
        public string? GenderString { get; set; }

        

        public required AccountStatus AccountStatus { get; set; }
        public required string? AccountStatusString { get; set; }

        public required Roles Role { get; set; }
        public required string? RoleString { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        [Phone]
        public required string Phone { get; set; }
       
        public required DateOnly BirthDate { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        public string? AccountStatusReason { get; set; }
        
     
        
    }
}
