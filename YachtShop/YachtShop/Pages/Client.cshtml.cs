using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace YachtShop.Pages
{
    public class ClientModel : PageModel
    {
        public string WelcomeString { get; set; }
        public void OnGet()
        {
            WelcomeString = "Welcome test";
        }
    }
}