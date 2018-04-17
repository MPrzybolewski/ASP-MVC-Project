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
    public class YachtController : Controller
    {
        private readonly IYachtRepository _yachtRepository;

        public YachtController(IYachtRepository yachtRepository)
        {
            _yachtRepository = yachtRepository;
        }

        // GET: Yacht
        public async Task<IActionResult> Index()
        {
            return View(await _yachtRepository.GetAll());
        }

        // GET: Yacht/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yacht = await _yachtRepository.GetById(id);
            if (yacht == null)
            {
                return NotFound();
            }

            return View(yacht);
        }

        // GET: Yacht/Create
        [Authorize(Roles = "Administrator, Seller")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Yacht/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Seller")]
        public async Task<IActionResult> Create([Bind("YachtId,Name,Price,Description")] Yacht yacht)
        {
            if (ModelState.IsValid)
            {
                _yachtRepository.Add(yacht);
                return RedirectToAction(nameof(Index));
            }
            return View(yacht);
        }

        // GET: Yacht/Edit/5
        [Authorize(Roles = "Administrator, Seller")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yacht = await _yachtRepository.GetById(id);
            if (yacht == null)
            {
                return NotFound();
            }
            return View(yacht);
        }

        // POST: Yacht/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Seller")]
        public async Task<IActionResult> Edit(string id, [Bind("YachtId,Name,Price,Description")] Yacht yacht)
        {
            if (id != yacht.YachtId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _yachtRepository.Update(yacht);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var temp = await YachtExists(yacht.YachtId);
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
            return View(yacht);
        }

        // GET: Yacht/Delete/5
        [Authorize(Roles = "Administrator, Seller")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yacht = await _yachtRepository.GetById(id);
            if (yacht == null)
            {
                return NotFound();
            }

            return View(yacht);
        }

        // POST: Yacht/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Seller")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var yacht = await _yachtRepository.GetById(id);
            _yachtRepository.Delete(yacht);
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View(yacht);
            }
        }

        private async Task<bool> YachtExists(string id)
        {
            var seller = await _yachtRepository.GetById(id);
            if (seller == null)
            {
                return false;
            }
            return true;
        }
    }
}
