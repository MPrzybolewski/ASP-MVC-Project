using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using YachtShop.Models;

namespace YachtShop.Pages.Purchases
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
        ViewData["ClientId"] = new SelectList(
                (from c in _context.Clients
                 select new
                 {
                     c.ClientId,
                     FullName = c.FirstName + " " + c.SecondName
                 }),
                "ClientId", "FullName", null);
            ViewData["SellerId"] = new SelectList(
                (from s in _context.Sellers
                 select new
                 {
                     s.SellerId,
                     FullName = s.FirstName + " " + s.SecondName
                 }),
                "SellerId", "FullName", null);
            ViewData["YachtId"] = new SelectList(_context.Yachts, "YachtId", "Name");
            return Page();
        }

        [BindProperty]
        public Purchase Purchase { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Purchases.Add(Purchase);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}