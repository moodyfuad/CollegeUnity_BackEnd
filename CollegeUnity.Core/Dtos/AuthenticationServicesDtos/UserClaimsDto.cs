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
        public string? PicturePath { get; set; }

        public string FullName { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public string? GenderString 
        {
            get
            {
                 switch (Gender)
                {
                    case Gender.Male:
                        return "Male";
                    case Gender.Female:
                        return "Femail";
                    default: return "";
                }

            }
        }

        public required List<Roles> Role { get; set; }
        public string? RoleString  => GetUserRoleName(Role); 

        private string GetUserRoleName(List<Roles> roles)
        {
            string rolesAsString = string.Empty;
                    foreach (var role in roles)
                    {
                        rolesAsString += role.AsString() + ",";
                    }

            return rolesAsString;
            
        }
        //[Required]
        //public required string FirstName { get; set; }
        //[Required]
        //public required string MiddleName { get; set; }
        //[Required]
        //public required string LastName { get; set; }

        //public string FullName
        //{
        //    get { return $"{FirstName} {MiddleName} {LastName}"; }
        //}



        //public required AccountStatus AccountStatus { get; set; }
        //public required string? AccountStatusString { get; set; }
        //[Required]
        //[EmailAddress]
        //public required string Email { get; set; }
        //[Required]
        //[Phone]
        //public required string Phone { get; set; }

        //public required DateOnly BirthDate { get; set; }

        //public DateTime CreatedAt { get; set; }

        //[Required]
        //public string? AccountStatusReason { get; set; }



    }
}
