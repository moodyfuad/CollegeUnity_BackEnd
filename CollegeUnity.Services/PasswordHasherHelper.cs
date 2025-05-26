using CollegeUnity.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services
{
    internal static class PasswordHasherHelper
    {
        private static readonly PasswordHasher<User> Hasher = new();

        public static string Hash(User user, string password)
        {
            return Hasher.HashPassword(user, password);
        }

        public static bool VerifyPassword(User user, string password)
        {
            return Hasher.VerifyHashedPassword(user, user.Password, password) switch
            {
                PasswordVerificationResult.Success => true,
                PasswordVerificationResult.SuccessRehashNeeded => true,
                _ => false
            };
        }
    }
}
