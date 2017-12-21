using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YachtShop.Models;

namespace YachtShop.Pages.Yachts
{
    public class EditModel : PageModel
    {
        private readonly YachtShop.Models.DatabaseContext _context;

        public EditModel(YachtShop.Models.DatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Yacht).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YachtExists(Yacht.YachtId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool YachtExists(int id)
        {
            return _context.Yachts.Any(e => e.YachtId == id);
        }
    }
}
