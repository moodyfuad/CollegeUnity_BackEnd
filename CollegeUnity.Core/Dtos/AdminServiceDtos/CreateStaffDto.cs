using CollegeUnity.Core.Dtos.AuthenticationDtos;
using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.AdminServiceDtos
{
    public class CreateStaffDto : UserSignUpDto
    {
        [Required(ErrorMessage = "Roles Field is required")]
        public required ICollection<Roles> Roles { get; set; }

        [Required(ErrorMessage = "Education Degree is required")]
        public required EducationDegree EducationDegree { get; set; }

    }
}
