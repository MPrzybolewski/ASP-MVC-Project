using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace YachtShop.Models.RoleViewModels
{
    public class RoleViewModel
    {
        public ApplicationUser ApplicationUser { get; set; }

        public string ApplicationUserId { get; set; }

        public string RoleId { get; set; }

        public string UserRole { get; set; }

        public IEnumerable<IdentityRole> Roles { get; set;}

    }
}
