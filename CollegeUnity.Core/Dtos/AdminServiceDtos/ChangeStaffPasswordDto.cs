using CollegeUnity.Core.CustomValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.AdminServiceDtos
{
    public class ChangeStaffPasswordDto
    {
        [Required(ErrorMessage = "Password is required")]
        [ContainsDigit(minNumberOfDigits: 1)]
        [ContainsLowerCase(minNumberOfLowerCase: 1)]
        [ContainsUpperCase(maxNumberOfUpperCase: 1)]
        [MinLength(8, ErrorMessage = "password must have at least 8 characters")]
        public string password { get; set; }
    }
}
