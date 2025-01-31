using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.AuthenticationDtos
{
    public class StudentSignUpDto : UserSignUpDto
    {
        [Length(11, 11)]
        [Required(ErrorMessage = "Student Id number is required")]
        public required string CardId { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        [Url]
        public required string CardIdPicturePath { get; set; }

        [Required(ErrorMessage = "Major is required")]
        public required Major Major { get; set; }
        [Required(ErrorMessage = "Level is required")]
        public required Level Level { get; set; }
        [Required(ErrorMessage = "Acceptance Type is required")]
        public required AcceptanceType AcceptanceType { get; set; }
    }
}
