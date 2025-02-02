using CollegeUnity.Core.Constants.AuthenticationConstants;
using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.AuthenticationDtos
{
    public class StaffClaimsDto : UserClaimsDto
    {
        public required EducationDegree EducationDegree { get; set; }


        public static StaffClaimsDto MapFromClaims(IEnumerable<Claim> claims)
        {
            return new()
            {
                Id = int.Parse(claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Id)!.Value),
                FirstName = claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.FirstName)!.Value,
                MiddleName = claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.MiddleName)!.Value,
                LastName = claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.LastName)!.Value,
                Phone = claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.PhoneNumber)!.Value,
                Email = claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Email)!.Value,
                BirthDate = DateOnly.Parse(claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.BirthDate)!.Value),
                Gender = (Gender)Enum.Parse(typeof(Gender), claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Gender)!.Value),

                Role = claims.Where(c => c.Type == CustomClaimTypes.Role)
               .Select(c => Enum.Parse<Roles>(c.Value))
               .ToList(),

                RoleString = claims.FirstOrDefault(c => c.Type == CustomClaimTypes.RoleName)!.Value,

                AccountStatus = (AccountStatus)Enum.Parse(typeof(AccountStatus), claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.AccountStatus)!.Value),
                AccountStatusString =claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.AccountStatus)!.Value,
                
                EducationDegree = (EducationDegree)Enum.Parse(typeof(EducationDegree), claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.EducationDegree)!.Value)
            };
        }
    }
}
