using CollegeUnity.Core.CustomValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.AuthenticationDtos
{
    public class StudentLoginDto : UserLoginDto
    {
        [DisplayName("Registration ID")]
        [Required(ErrorMessage = "Student Id number is required")]
        [Length(11,11,ErrorMessage = "Registration ID must be 11 digits")]
        [NumbersOnly]
        public string? CardId { get; set; }   
    }
}
