using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YachtShop.Data.Repositories.Interfaces;
using YachtShop.Models;

namespace YachtShop.Data.Repositories
{
    public class SellerRepository : ISellerRepository
    {
        private readonly ApplicationDbContext _context;

        public SellerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Seller seller)
        {
            _context.Add(seller);
        }

        public void Delete(Seller seller)
        {
            _context.Remove(seller);
        }

        public async Task<IEnumerable<Seller>> GetAll()
        {
            return await _context.Sellers.ToListAsync();
        }

        public async Task<Seller> GetById(string id)
        {
            return await _context.Sellers.FirstOrDefaultAsync(x => x.SellerId == id);
        }

        public void Update(Seller seller)
        {
            _context.Update(seller);
        }

    }
}
