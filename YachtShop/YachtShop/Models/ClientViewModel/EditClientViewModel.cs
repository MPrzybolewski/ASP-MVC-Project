using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YachtShop.Models.ClientViewModel
{
    public class EditClientViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and max {1} characters long", MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and max {1} characters long", MinimumLength = 3)]
        [Display(Name = "Second Name")]
        public string SecondName { get; set; }
        [Required]
        [StringLength(9, ErrorMessage = "The {0} must be at least {2} and max {1} characters long", MinimumLength = 9)]
        [Display(Name = "Second Name")]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Second Name")]
        public string Email { get; set; }
    }
}
