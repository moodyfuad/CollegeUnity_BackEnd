using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.AuthenticationDtos
{
    public abstract class AuthenticationUserDto : User
    {

        public string FullName
        {
            get { return $"{FirstName} {MiddleName} {LastName}"; }
        }
        public required Roles Role { get; set; }

        public required Gender Gender { get; set; }
    }
}
