using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using YachtShop.Models;

namespace YachtShop.Pages.Yachts
{
    public class DetailsModel : PageModel
    {
        private readonly YachtShop.Models.DatabaseContext _context;

        public DetailsModel(YachtShop.Models.DatabaseContext context)
        {
            _context = context;
        }

        public Yacht Yacht { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Yacht = await _context.Yachts.SingleOrDefaultAsync(m => m.YachtId == id);

            if (Yacht == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
