using CollegeUnity.Core.Constants.AuthenticationConstants;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.AuthenticationDtos
{
    public class StudentClaimsDto : UserClaimsDto
    {
        public required string CardId { get; set; }

        //public required Major Major { get; set; }
        //public required string MajorString { get; set; }

        //public required Level Level { get; set; }
        //public required string LevelString { get; set; }
       
        //public required AcceptanceType AcceptanceType { get; set; }
        //public required string AcceptanceTypeString { get; set; }


        public static StudentClaimsDto FromClaims(IEnumerable<Claim> claims)
        {
            return new()
            {
                PicturePath = claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.PicturePath)?.Value ?? string.Empty,

                Id = int.Parse(claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Id)!.Value),

                CardId = claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.RegistrationID)!.Value,
                
                Gender = (Gender)Enum.Parse(typeof(Gender), claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Gender)!.Value),

                Role = claims.Where(c => c.Type == CustomClaimTypes.Role)
               .Select(c => Enum.Parse<Roles>(c.Value))
               .ToList(),


                //student

                //AcceptanceType = (AcceptanceType)Enum.Parse(typeof(AcceptanceType), claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.AcceptanceType)!.Value),
                //AcceptanceTypeString =claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.AcceptanceType)!.Value,

                //Major = (Major)Enum.Parse(typeof(Major), claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Major)!.Value),
                //MajorString =claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Major)!.Value,

                //Level = (Level)Enum.Parse(typeof(Level), claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Level)!.Value),
                //LevelString =claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Level)!.Value

            };
        }
    }

}
