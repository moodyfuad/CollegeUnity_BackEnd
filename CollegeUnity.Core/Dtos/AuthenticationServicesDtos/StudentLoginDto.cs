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
        public StudentLoginDto()
        {
        }
        private StudentLoginDto(string Token) : base(Token) { }

        private StudentLoginDto(params string[] errors) : base(errors) { }
        
        public static StudentLoginDto Failed(params string[] errors)
        {
             return new(errors);
        }
        public static StudentLoginDto Success(string token)
        {
             return new(token);
        }

        [Required(ErrorMessage = "Student Id number is required")]
        [MaxLength(11, ErrorMessage = "Student Id is Not valid")]
        [MinLength(11,ErrorMessage = "Student Id is Not valid")]
        public string? CardId { get; set; }

        
    }
}
