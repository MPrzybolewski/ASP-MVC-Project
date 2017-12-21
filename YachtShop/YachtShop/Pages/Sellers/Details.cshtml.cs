using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using YachtShop.Models;

namespace YachtShop.Pages.Sellers
{
    public class DetailsModel : PageModel
    {
        private readonly YachtShop.Models.DatabaseContext _context;

        public DetailsModel(YachtShop.Models.DatabaseContext context)
        {
            _context = context;
        }

        public Seller Seller { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Seller = await _context.Sellers.SingleOrDefaultAsync(m => m.SellerId == id);

            if (Seller == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
