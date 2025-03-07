using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.CustomValidationAttributes
{
    public class ContainsLowerCaseAttribute : ValidationAttribute
    {
        private readonly uint? minNumberOfLowerCase;
        private readonly uint? maxNumberOfLowerCase;
        private readonly string? errorMessage;

        public ContainsLowerCaseAttribute(uint minNumberOfLowerCase = 1, uint maxNumberOfLowerCase = default, string? errorMessage = null)
        {
            this.minNumberOfLowerCase = minNumberOfLowerCase;
            this.maxNumberOfLowerCase = maxNumberOfLowerCase == default ? null : maxNumberOfLowerCase;
            this.errorMessage = errorMessage;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string? displayName = validationContext.DisplayName ?? validationContext.MemberName;
            string strValue = value.ToString()!;
            int numberOfLowerCase = strValue.Count(char.IsLower);

            if (numberOfLowerCase < minNumberOfLowerCase)
            {
                return new ValidationResult(errorMessage ?? $"{displayName} must contains at least [ {minNumberOfLowerCase} ] LowerCase characters");
            }

            if (maxNumberOfLowerCase.HasValue && numberOfLowerCase > maxNumberOfLowerCase)
            {
                return new ValidationResult(errorMessage ?? $"{displayName} must contains at most [ {maxNumberOfLowerCase} ] LowerCase characters");
            }

            return ValidationResult.Success;
        }
    }
}
