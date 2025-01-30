using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.AuthenticationDtos
{
    public class StaffClaimsDto : AuthenticationUserDto
    {
        public required EducationDegree EducationDegree { get; set; }
    }
}
