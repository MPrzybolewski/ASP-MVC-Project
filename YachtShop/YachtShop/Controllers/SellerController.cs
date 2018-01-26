using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YachtShop.Data;
using YachtShop.Models;

namespace YachtShop.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SellerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SellerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Seller
        public async Task<IActionResult> Index(string searchString)
        {
            return View(await _context.Sellers.ToListAsync());
        }

        // GET: Seller/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .SingleOrDefaultAsync(m => m.SellerId == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // GET: Seller/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Seller/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SellerId,FirstName,SecondName,Salary,PhoneNumber,Email")] Seller seller)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seller);
        }

        // GET: Seller/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers.SingleOrDefaultAsync(m => m.SellerId == id);
            if (seller == null)
            {
                return NotFound();
            }
            return View(seller);
        }

        // POST: Seller/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SellerId,FirstName,SecondName,Salary,PhoneNumber,Email")] Seller seller)
        {
            if (id != seller.SellerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seller);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellerExists(seller.SellerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(seller);
        }

        // GET: Seller/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .SingleOrDefaultAsync(m => m.SellerId == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // POST: Seller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var seller = await _context.Sellers.SingleOrDefaultAsync(m => m.SellerId == id);
            _context.Sellers.Remove(seller);
            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View(seller);
            }
        }

        private bool SellerExists(string id)
        {
            return _context.Sellers.Any(e => e.SellerId == id);
        }
    }
}
