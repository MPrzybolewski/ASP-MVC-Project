using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YachtShop.Data.Repositories.Interfaces;
using YachtShop.Models;

namespace YachtShop.Data.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        ApplicationDbContext _context;

        public PurchaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Purchase purchase)
        {
            _context.Purchases.Add(purchase);
        }

        public void Delete(Purchase purchase)
        {
            _context.Purchases.Remove(purchase);
        }

        public async Task<IEnumerable<Purchase>> GetAll()
        {
           return await _context.Purchases.Include(x => x.Client).Include(x => x.Seller).Include(x => x.Yacht).ToListAsync();
        }

        public async Task<Purchase> GetById(string id)
        {
            return await _context.Purchases.Include(x => x.Client).
                                 Include(x => x.Seller).Include(x => x.Yacht).
                                 FirstOrDefaultAsync(x => x.PurchaseId == id);
        }

        public void Update(Purchase purchase)
        {
            _context.Purchases.Update(purchase);
        }
    }
}
