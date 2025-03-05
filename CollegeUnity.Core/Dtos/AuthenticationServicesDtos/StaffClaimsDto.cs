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

                PicturePath = claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.PicturePath)?.Value ?? string.Empty,

                Id = int.Parse(claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Id)!.Value),
                Gender = (Gender)Enum.Parse(typeof(Gender), claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Gender).Value),

                FullName = claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.FullName)?.Value ?? "",

                EducationDegree = (EducationDegree)Enum.Parse(typeof(EducationDegree), claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.EducationDegree)!.Value),

                Role = claims.Where(c => c.Type == CustomClaimTypes.Role)
               .Select(c => Enum.Parse<Roles>(c.Value))
               .ToList(),

            };
        }
    }
}
