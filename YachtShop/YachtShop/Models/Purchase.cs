using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YachtShop.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public int ClientId { get; set; }
        public int SellerId { get; set; }
        public int YachtId { get; set; }
        public DateTime PurchaseDate { get; set; }

        public Client Client { get; set; }
        public Seller Seller { get; set; }
        public Yacht Yacht { get; set; }
    }
}
