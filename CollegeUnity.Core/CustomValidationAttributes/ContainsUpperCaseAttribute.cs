using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.CustomValidationAttributes
{
    public class ContainsUpperCaseAttribute : ValidationAttribute
    {
        private readonly uint? minNumberOfUpperCase;
        private readonly uint? maxNumberOfUpperCase;
        private readonly string? errorMessage;

        public ContainsUpperCaseAttribute(uint minNumberOfUpperCase = 1, uint maxNumberOfUpperCase = default, string? errorMessage = null) : base()
        {
            this.minNumberOfUpperCase = minNumberOfUpperCase;
            this.maxNumberOfUpperCase = maxNumberOfUpperCase == default ? null: maxNumberOfUpperCase;
            this.errorMessage = errorMessage;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string? displayName = validationContext.DisplayName ?? validationContext.MemberName;
            string strValue = value.ToString()!;
            int numberOfUppercase = strValue.Count(char.IsUpper);

            if (numberOfUppercase < minNumberOfUpperCase)
            {
                return new ValidationResult(errorMessage ?? $"{displayName} must contains at least [ {minNumberOfUpperCase} ] uppercase characters");
            }

            if (maxNumberOfUpperCase.HasValue && numberOfUppercase > maxNumberOfUpperCase)
            {
                return new ValidationResult(errorMessage ?? $"{displayName} must contains at most [ {maxNumberOfUpperCase} ] uppercase characters");
            }

            return ValidationResult.Success;
        }
    }
}
