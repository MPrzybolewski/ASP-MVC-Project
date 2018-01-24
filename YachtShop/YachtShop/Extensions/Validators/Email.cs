using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YachtShop.Extensions.Validators
{
    public class Email : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(validationContext.DisplayName + " is required.");
            }

            var email = value.ToString();
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return ValidationResult.Success;
            }
            catch
            {
                return new ValidationResult("Invalid email(use proper format).");
            }
        }
    }
}
