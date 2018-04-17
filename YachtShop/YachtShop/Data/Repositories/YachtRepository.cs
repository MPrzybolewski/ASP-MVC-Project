using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YachtShop.Data.Repositories.Interfaces;
using YachtShop.Models;

namespace YachtShop.Data.Repositories
{
    public class YachtRepository : IYachtRepository
    {
        private readonly ApplicationDbContext _context;

        public YachtRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Yacht yacht)
        {
            _context.Add(yacht);
        }

        public void Delete(Yacht yacht)
        {
            _context.Remove(yacht);
        }

        public async Task<IEnumerable<Yacht>> GetAll()
        {
            return await _context.Yachts.ToListAsync();
        }

        public async Task<Yacht> GetById(string id)
        {
            return await _context.Yachts.FirstOrDefaultAsync(x => x.YachtId == id);
        }

        public void Update(Yacht yacht)
        {
            _context.Update(yacht);
        }
    }
}
