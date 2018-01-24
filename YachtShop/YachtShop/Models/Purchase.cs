using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YachtShop.Models
{
    public class Purchase
    {
        [Key]
        public string PurchaseId { get; set; }
        
        [Display(Name="Client")]
        public string ClientId { get; set; }

        [Display(Name = "Seller")]
        public string SellerId { get; set; }

        [Display(Name = "Yacht")]
        public string YachtId { get; set; }

        [Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; }

        public Client Client { get; set; }
        public Seller Seller { get; set; }
        public Yacht Yacht { get; set; }
    }
}
