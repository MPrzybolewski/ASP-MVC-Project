using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YachtShop.Models
{
    public class Seller
    {
        public int SellerId { get; set; }
        [Required(ErrorMessage = "Enter First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter Second Name")]
        public string SecondName { get; set; }
        [Required(ErrorMessage = "Enter Salary")]
        public decimal Salary { get; set; }
        [Required(ErrorMessage = "Enter Phone Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Enter Email")]
        public string Email { get; set; }

    }
}
