using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.AuthenticationDtos
{
    public abstract class UserLoginDto
    {
       
        [Required]
        [MinLength(4)]
        public required string Password { get; set; }

    }
}
