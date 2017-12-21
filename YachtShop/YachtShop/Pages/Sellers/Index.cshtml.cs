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
    public class IndexModel : PageModel
    {
        private readonly YachtShop.Models.DatabaseContext _context;

        public IndexModel(YachtShop.Models.DatabaseContext context)
        {
            _context = context;
        }

        public IList<Seller> Seller { get;set; }

        public async Task OnGetAsync()
        {
            Seller = await _context.Sellers.ToListAsync();
        }
    }
}
