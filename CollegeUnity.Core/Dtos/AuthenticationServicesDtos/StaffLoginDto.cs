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
        private StaffLoginDto(string Token) : base(Token) { }

        private StaffLoginDto(params string[] errors) : base(errors) { }

        [EmailAddress(ErrorMessage = "Not vailed Email")]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        public static StaffLoginDto Failed(params string[] errors)
        {
            return new(errors);
        }
        public static StaffLoginDto Success(string token)
        {
            return new(token);
        }
    }
}
