using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.AuthenticationDtos
{
    public class StaffLoginDto : UserLoginDto
    {
        [EmailAddress(ErrorMessage = "Not vailed Email")]
        [Required(ErrorMessage = "Email is required")]
        public required string Email { get; set; }
    }
}
