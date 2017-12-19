using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YachtShop.Models;

namespace YachtShop.Pages
{
    public class ClientModel : PageModel
    {
        public Client Client { get; set; }
        public void OnGet()
        {
            
        }
    }
}