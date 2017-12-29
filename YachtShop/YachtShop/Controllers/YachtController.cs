using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YachtShop.Data;
using YachtShop.Models;

namespace YachtShop.Controllers
{
    public class YachtController : Controller
    {
        private readonly ApplicationDbContext _context;

        public YachtController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Yacht
        public async Task<IActionResult> Index()
        {
            return View(await _context.Yachts.ToListAsync());
        }

        // GET: Yacht/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yacht = await _context.Yachts
                .SingleOrDefaultAsync(m => m.YachtId == id);
            if (yacht == null)
            {
                return NotFound();
            }

            return View(yacht);
        }

        // GET: Yacht/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Yacht/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("YachtId,Name,Price,Description")] Yacht yacht)
        {
            if (ModelState.IsValid)
            {
                _context.Add(yacht);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(yacht);
        }

        // GET: Yacht/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yacht = await _context.Yachts.SingleOrDefaultAsync(m => m.YachtId == id);
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
                    _context.Update(yacht);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YachtExists(yacht.YachtId))
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
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yacht = await _context.Yachts
                .SingleOrDefaultAsync(m => m.YachtId == id);
            if (yacht == null)
            {
                return NotFound();
            }

            return View(yacht);
        }

        // POST: Yacht/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var yacht = await _context.Yachts.SingleOrDefaultAsync(m => m.YachtId == id);
            _context.Yachts.Remove(yacht);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YachtExists(string id)
        {
            return _context.Yachts.Any(e => e.YachtId == id);
        }
    }
}
