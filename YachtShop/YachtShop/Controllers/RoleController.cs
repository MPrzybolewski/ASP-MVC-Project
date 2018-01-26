using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YachtShop.Data;
using YachtShop.Models;
using YachtShop.Models.RoleViewModels;

namespace YachtShop.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var allRoles = await _context.Roles.ToListAsync();
            var allUsers = await _context.ApplicationUsers.ToListAsync();

            var model = allUsers.Select(user =>
            {
                var userRole = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
                
                var roleViewModel = new RoleViewModel
                {
                    ApplicationUser = user,
                    ApplicationUserId = user.Id,
                    Roles = allRoles,
                    UserRole = userRole
                };
                return roleViewModel;
            });

            return View(model);
        }

    }
}
