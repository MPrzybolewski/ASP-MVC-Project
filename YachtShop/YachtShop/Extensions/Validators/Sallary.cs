using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YachtShop.Extensions.Validators
{
    public class Sallary : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(validationContext.DisplayName + " is required.");
            }

            decimal salary =  (Decimal)value;
            if (salary >= 1900 && salary <= 5000)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Sallary for Seller must be between 1900 and 5000");
        }
    }
}
