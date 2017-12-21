using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using YachtShop.Models;

namespace YachtShop.Pages.Yachts
{
    public class CreateModel : PageModel
    {
        private readonly YachtShop.Models.DatabaseContext _context;

        public CreateModel(YachtShop.Models.DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Yacht Yacht { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Yachts.Add(Yacht);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}