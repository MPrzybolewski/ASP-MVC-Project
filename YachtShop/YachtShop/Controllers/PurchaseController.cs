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
using YachtShop.Data.UnitOfWork.Abstraction;
using YachtShop.Models;

namespace YachtShop.Controllers
{
    [Authorize(Roles = "Administrator, Seller")]
    public class PurchaseController : Controller
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ISellerRepository _sellerRepository;
        private readonly IYachtRepository _yachtRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseController(IPurchaseRepository purchaseRepository, IClientRepository clientRepository,
                                  ISellerRepository sellerRepository, IYachtRepository yachtRepository, IUnitOfWork unitOfWork)
        {
            _purchaseRepository = purchaseRepository;
            _clientRepository = clientRepository;
            _sellerRepository = sellerRepository;
            _yachtRepository = yachtRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: Purchase
        public async Task<IActionResult> Index()
        {   
            return View(await _purchaseRepository.GetAll());
        }

        // GET: Purchase/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _purchaseRepository.GetById(id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // GET: Purchase/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ClientId"] = new SelectList( await _clientRepository.GetAll(),
                "ClientId", "FullName", null);
            ViewData["SellerId"] = new SelectList( await _sellerRepository.GetAll(),
                "SellerId", "FullName", null);
            ViewData["YachtId"] = new SelectList( await _yachtRepository.GetAll(),
                "YachtId", "FullView", null);
            return View();
        }

        // POST: Purchase/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurchaseId,ClientId,SellerId,YachtId,PurchaseDate")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                _purchaseRepository.Add(purchase);
                await _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList( await _clientRepository.GetAll(),
                "ClientId", "FullName", null);
            ViewData["SellerId"] = new SelectList( await _sellerRepository.GetAll(),
                "SellerId", "FullName", null);
            ViewData["YachtId"] = new SelectList( await _yachtRepository.GetAll(),
                "YachtId", "FullView", null);
            return View(purchase);
        }

        // GET: Purchase/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _purchaseRepository.GetById(id);
            if (purchase == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList( await _clientRepository.GetAll(),
                "ClientId", "FullName", null);
            ViewData["SellerId"] = new SelectList( await _sellerRepository.GetAll(),
                "SellerId", "FullName", null);
            ViewData["YachtId"] = new SelectList( await _yachtRepository.GetAll(),
                "YachtId", "FullView", null);
            return View(purchase);
        }

        // POST: Purchase/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PurchaseId,ClientId,SellerId,YachtId,PurchaseDate")] Purchase purchase)
        {
            if (id != purchase.PurchaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _purchaseRepository.Update(purchase);
                    await _unitOfWork.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    var temp = await PurchaseExists(purchase.PurchaseId);
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
            ViewData["ClientId"] = new SelectList(await _clientRepository.GetAll(),
                "ClientId", "FullName", null);
            ViewData["SellerId"] = new SelectList(await _sellerRepository.GetAll(),
                "SellerId", "FullName", null);
            ViewData["YachtId"] = new SelectList(await _yachtRepository.GetAll(),
                "YachtId", "FullView", null);
            return View(purchase);
        }

        // GET: Purchase/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _purchaseRepository.GetById(id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // POST: Purchase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var purchase = await _purchaseRepository.GetById(id);
            _purchaseRepository.Delete(purchase);
            await _unitOfWork.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PurchaseExists(string id)
        {
            var purchase = await _purchaseRepository.GetById(id);
            if (purchase == null)
            {
                return false;
            }
            return true;
        }
    }
}
