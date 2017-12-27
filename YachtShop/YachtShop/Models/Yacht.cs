using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YachtShop.Models
{
    public class Yacht
    {
        public string YachtId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public ICollection<Purchase> Purchases { get; set; }
    }
}
