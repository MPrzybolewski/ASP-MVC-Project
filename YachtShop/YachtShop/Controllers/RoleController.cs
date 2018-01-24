using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YachtShop.Data;
using YachtShop.Models;
using YachtShop.Models.RoleViewModels;

namespace YachtShop.Controllers
{
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

        // GET: Role/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.ApplicationUsers.SingleOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            var allRoles = await _context.Roles.ToListAsync();
            var userRole = _userManager.GetRolesAsync(user).Result.FirstOrDefault();

            var roleViewModel = new RoleViewModel
            {
                ApplicationUser = user,
                ApplicationUserId = user.Id,
                Roles = allRoles,
                UserRole = userRole
            };


            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "RoleId", userRole);
            return View(roleViewModel);
        }

        // POST: Role/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleViewModel model)
        {

            if (ModelState.IsValid)
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "RoleId", model.UserRole);
            return View(model);
        }


    }
}
