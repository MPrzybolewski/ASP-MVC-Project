using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YachtShop.Data;
using YachtShop.Data.Repositories.Interfaces;
using YachtShop.Models;

namespace YachtShop.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SellerController : Controller
    {
        private readonly ISellerRepository _sellerRepostory;

        public SellerController(ISellerRepository sellerRepository)
        {
            _sellerRepostory = sellerRepository;
        }

        // GET: Seller
        public async Task<IActionResult> Index(string searchString)
        {
            return View(await _sellerRepostory.GetAll());
        }

        // GET: Seller/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _sellerRepostory.GetById(id);
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
                _sellerRepostory.Add(seller);
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

            var seller = await _sellerRepostory.GetById(id);
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
                    _sellerRepostory.Update(seller);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var temp = await SellerExists(seller.SellerId);
                    if (!temp)
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

            var seller = await _sellerRepostory.GetById(id);
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
            var seller = await _sellerRepostory.GetById(id);
            _sellerRepostory.Delete(seller);
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View(seller);
            }
        }

        private async Task<bool> SellerExists(string id)
        {
            var seller = await _sellerRepostory.GetById(id);
            if (seller == null)
            {
                return false;
            }
            return true;
        }
    }
}
