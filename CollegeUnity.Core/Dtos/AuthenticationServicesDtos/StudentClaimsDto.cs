using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.AuthenticationDtos
{
    public class StudentClaimsDto : AuthenticationUserDto
    {
        public required string CardId { get; set; }

        public required Major Major { get; set; }

        public required Level Level { get; set; }
       
        public required AcceptanceType AcceptanceType { get; set; }

    }
}
