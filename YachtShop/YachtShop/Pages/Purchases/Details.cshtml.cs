using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using YachtShop.Models;

namespace YachtShop.Pages.Purchases
{
    public class DetailsModel : PageModel
    {
        private readonly YachtShop.Models.DatabaseContext _context;

        public DetailsModel(YachtShop.Models.DatabaseContext context)
        {
            _context = context;
        }

        public Purchase Purchase { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Purchase = await _context.Purchases
                .Include(p => p.Client)
                .Include(p => p.Seller)
                .Include(p => p.Yacht).SingleOrDefaultAsync(m => m.PurchaseId == id);

            if (Purchase == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
