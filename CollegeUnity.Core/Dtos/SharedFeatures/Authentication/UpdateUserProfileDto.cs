using CollegeUnity.Core.CustomValidationAttributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.SharedFeatures.Authentication
{
    public class UpdateUserProfileDto
    {
        [AllowNull]
        public IFormFile? ProfilePicture { get; set; } = null;
        [EmailAddress()]
        [Required]
        [AllowNull]
        public string? Email { get; set; } = null;
        [NumbersOnly]
        [Phone]
        [AllowNull]
        public string? Phone { get; set; } = null;

        public bool UpdateImage { get; set; } = false;
    }
}
