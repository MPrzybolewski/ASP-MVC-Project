using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YachtShop.Models
{
    public class Purchase
    {
        public string PurchaseId { get; set; }
        public string ClientId { get; set; }
        public string SellerId { get; set; }
        public string YachtId { get; set; }
        public DateTime PurchaseDate { get; set; }

        public Client Client { get; set; }
        public Seller Seller { get; set; }
        public Yacht Yacht { get; set; }
    }
}
