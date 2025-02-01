using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Constants
{
    public class CustomClaimTypes
    {
        public const string Id = "Id";
        public const string CardId = "CardId";
        public const string AccountStatus = "AccountStatus";
        public const string Major = "Major";
        public const string Level = "Level";
        public const string AcceptanceType = "AcceptanceType";

        public const string EducationDegree = "EducationDegree";

        public const string Role = ClaimTypes.Role;
        public const string RoleName = "RoleName";
        public const string FirstName = "FirstName";
        public const string MiddleName = "MiddleName";
        public const string LastName = "LastName";
        public const string FullName = "FullName";
        public const string Gender = "Gender";
        public const string Username = "Username";
        public const string BirthDate = "BirthDate";
        public const string Email = "Email";
        public const string PhoneNumber = "PhoneNumber";
        public const string Position = "Position";
    }
}
