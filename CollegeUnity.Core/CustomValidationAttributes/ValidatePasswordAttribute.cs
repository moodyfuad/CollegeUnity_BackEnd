using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CollegeUnity.Core.CustomValidationAttributes
{
    public class ValidatePasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string? displayName = validationContext.DisplayName ?? validationContext.MemberName;
            string strValue = value.ToString()!;

            if (strValue.Length < 8)
            {
                return new ValidationResult($"{displayName} must be 8 characters minimums");
            }

            // Check if strValue contains at least one numeric character
            if (!Regex.IsMatch(strValue, @"\d"))
            {
                return new ValidationResult($"{displayName} must contains at least one numeric character");
            }

            // Check if strValue contains at least one uppercase letter
            if (!Regex.IsMatch(strValue, @"[A-Z]"))
            {
                return new ValidationResult($"{displayName} must contains at least one uppercase character");
            }

            // Check if strValue contains at least one lowercase letter
            if (!Regex.IsMatch(strValue, @"[a-z]"))
            {
                return new ValidationResult($"{displayName} must contains at least one lowercase character");
            }
            return  ValidationResult.Success;
        }
    }
}
