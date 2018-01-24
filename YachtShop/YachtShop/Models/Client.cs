using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YachtShop.Extensions.Validators;

namespace YachtShop.Models
{
    public class Client
    {
        [Key]
        public string ClientId { get; set; }

        [Display (Name = "First Name")]
        [Required(ErrorMessage = "Enter First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Second Name")]
        [Required(ErrorMessage = "Enter Second Name")]
        public string SecondName { get; set; }

        [PhoneNumber]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Enter Phone Number")]
        public string PhoneNumber { get; set; }

        [Email]
        [Required(ErrorMessage = "Enter Email")]
        public string Email { get; set; }

        public ICollection<Purchase> Purchases { get; set; }


    }
}
