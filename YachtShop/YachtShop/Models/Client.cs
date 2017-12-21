﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YachtShop.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        [Required(ErrorMessage = "Enter First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter Second Name")]
        public string SecondName { get; set; }
        [Required(ErrorMessage = "Enter Phone Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Enter Email")]
        public string Email { get; set; }

        public ICollection<Purchase> Purchases { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + SecondName;
            }
        }

    }
}