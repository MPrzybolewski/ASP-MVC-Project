using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YachtShop.Extensions.Validators;

namespace YachtShop.Models
{
    public class Seller
    {
        public string SellerId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Enter First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Second Name")]
        [Required(ErrorMessage = "Enter Second Name")]
        public string SecondName { get; set; }

        [Salary]
        [Required(ErrorMessage = "Enter Salary")]
        public decimal Salary { get; set; }

        [PhoneNumber]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Enter Phone Number")]
        public string PhoneNumber { get; set; }

        [Email]
        [Required(ErrorMessage = "Enter Email")]
        public string Email { get; set; }

        public string FullName
        {
            get { return FirstName + " " + SecondName; }
        }

        public ICollection<Purchase> Purchases { get; set; }

    }
}
