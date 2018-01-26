using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YachtShop.Extensions.Validators;

namespace YachtShop.Models
{
    public class Yacht
    {
        public string YachtId { get; set; }

        [Required(ErrorMessage = "Enter Yacht name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Yacht price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Enter Yacht description")]
        public string Description { get; set; }

        public ICollection<Purchase> Purchases { get; set; }

        public string FullView
        {
            get { return Name + " " + Price; }
        }
    }
}
