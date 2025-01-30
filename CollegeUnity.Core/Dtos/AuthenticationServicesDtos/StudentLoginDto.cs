using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.AuthenticationDtos
{
    public class StudentLoginDto : UserLoginDto
    {
        [Required(ErrorMessage = "Student Id number is required")]
        [MaxLength(11, ErrorMessage = "Student Id is Not valid")]
        [MinLength(11,ErrorMessage = "Student Id is Not valid")]
        public required string CardId { get; set; }
        
    }
}
