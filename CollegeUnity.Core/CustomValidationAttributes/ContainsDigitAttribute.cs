using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CollegeUnity.Core.CustomValidationAttributes
{
    public class ContainsDigitAttribute : ValidationAttribute
    {
        private readonly uint? minNumberOfDigits;
        private readonly uint? maxNumberOfDigits;
        private readonly string? errorMessage;
        public ContainsDigitAttribute() : base()
        {
            
        }
        public ContainsDigitAttribute(uint minNumberOfDigits = 1, uint maxNumberOfDigits = default, string? errorMessage = null)
        {
            this.minNumberOfDigits = minNumberOfDigits;
            this.maxNumberOfDigits = maxNumberOfDigits == default ? null: maxNumberOfDigits;
            this.errorMessage = errorMessage;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string? displayName = validationContext.DisplayName ?? validationContext.MemberName;
            string strValue = value.ToString()!;
            int numberOfDigits = strValue.Count(c => "1234567890".Contains(c));

            if (numberOfDigits < minNumberOfDigits)
            {
                return new ValidationResult(errorMessage ?? $"{displayName} must contains at least [ {minNumberOfDigits} ] numeric character");
            }

            if (maxNumberOfDigits.HasValue && numberOfDigits > maxNumberOfDigits)
            {
                return new ValidationResult(errorMessage ?? $"{displayName} must contains at most [ {maxNumberOfDigits} ] numeric character");
            }

            return ValidationResult.Success;
        }
    }
}
