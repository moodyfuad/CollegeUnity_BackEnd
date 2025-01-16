using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos
{
    public class UserLoginDto
    {
        [Required]
        [MinLength(4)]
        public string? Username { get; set; }
        [Required]
        public string? Password{ get; set; }
    }
}
