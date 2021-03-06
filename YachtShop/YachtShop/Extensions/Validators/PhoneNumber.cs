﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YachtShop.Extensions.Validators
{
    public class PhoneNumber : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(validationContext.DisplayName + " is required.");
            }

            var phone = value.ToString();
            if (Regex.IsMatch(phone, "^[0-9]{3}[-][0-9]{3}[-][0-9]{3}$"))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid phone number format (use format: xxx-xxx-xxx).");
        }
    }
}
