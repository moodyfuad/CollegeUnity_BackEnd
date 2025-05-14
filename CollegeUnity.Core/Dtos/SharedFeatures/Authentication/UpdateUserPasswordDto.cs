using CollegeUnity.Core.CustomValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.SharedFeatures.Authentication
{
    public class UpdateUserPasswordDto
    {
        [Required]   
        public string Old { get; set; }
        [Required]
        [MinLength(8)]
        [ContainsDigit]
        [ContainsLowerCase]
        [ContainsUpperCase]
        public string New { get; set; }
    }
}
