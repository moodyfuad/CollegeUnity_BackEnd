using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.AuthenticationDtos
{
    public abstract class UserLoginDto
    {
        
       
        [Required(ErrorMessage = "Password is required field")]
        [MinLength(4)]        
        public string? Password { get; set; }
       

    }
}
