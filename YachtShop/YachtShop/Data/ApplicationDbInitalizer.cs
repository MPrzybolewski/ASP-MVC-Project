using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using YachtShop.Models;

namespace YachtShop.Data
{
    public class ApplicationDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationDbInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void Seed()
        {
            _context.Database.Migrate();
                
            if (!_context.Roles.Any())
            {
                var roleNames = new[]
                {
                    Roles.Roles.Administrator,
                    Roles.Roles.Seller,
                    Roles.Roles.User
                };

                foreach (var roleName in roleNames)
                {
                    var role = new IdentityRole(roleName) { NormalizedName = roleName.ToUpper() };
                    _context.Roles.Add(role);
                }
            }

            if (!_context.ApplicationUsers.Any())
            {
                const string adminName = "admin@admin.pl";
                const string adminPass = "p@$$w0rd";

                var admin = new ApplicationUser { UserName = adminName, Email = adminName };
                _userManager.CreateAsync(admin, adminPass).Wait();
                _userManager.AddToRoleAsync(admin, Roles.Roles.Administrator).Wait();

                const string sellerName = "seller@seller.pl";
                const string sellerPass = "seller123";

                var seller = new ApplicationUser { UserName = sellerName, Email = sellerName };
                _userManager.CreateAsync(seller, sellerPass).Wait();
                _userManager.AddToRoleAsync(seller, Roles.Roles.Seller).Wait();

                const string userName = "user@user.pl";
                const string userPass = "user123";

                var user = new ApplicationUser { UserName = userName, Email = userName };
                _userManager.CreateAsync(user, userPass).Wait();
                _userManager.AddToRoleAsync(user, Roles.Roles.User).Wait();
            }

            _context.SaveChanges();
        }
    }
}
