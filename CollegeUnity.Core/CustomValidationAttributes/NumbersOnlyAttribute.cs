using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.CustomValidationAttributes
{
    public class NumbersOnlyAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string? displayName = validationContext.DisplayName ?? validationContext.MemberName;

            string strValue = value?.ToString() ?? string.Empty;

            if (strValue.Any(ch => !char.IsNumber(ch)))
            {
                return new ValidationResult($"{displayName} must be all numbers");
            }

            return ValidationResult.Success;
        }
    }
}
