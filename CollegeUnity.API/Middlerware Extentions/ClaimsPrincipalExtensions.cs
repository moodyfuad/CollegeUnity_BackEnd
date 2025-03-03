using CollegeUnity.Core.Constants.AuthenticationConstants;
using System.Security.Claims;

namespace CollegeUnity.API.Middlerware_Extentions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirstValue(CustomClaimTypes.Id);

            if (string.IsNullOrEmpty(userIdClaim))
            {
                throw new UnauthorizedAccessException("not found.");
            }

            if (!int.TryParse(userIdClaim, out int userId))
            {
                throw new InvalidOperationException("Invalid format.");
            }

            return userId;
        }
    }
}
