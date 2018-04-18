using System;
using System.Threading.Tasks;
using YachtShop.Data.UnitOfWork.Abstraction;

namespace YachtShop.Data.UnitOfWork
{
    public class EntityUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public EntityUnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
