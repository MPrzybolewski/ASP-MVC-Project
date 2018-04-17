using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YachtShop.Data.Repositories.Interfaces;
using YachtShop.Models;

namespace YachtShop.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Client client)
        {
            _context.Add(client);
        }

        public void Delete(Client client)
        {
            _context.Remove(client);
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client> GetById(string id)
        {
            return await _context.Clients.FirstOrDefaultAsync(x => x.ClientId == id);
        }

        public void Update(Client client)
        {
            _context.Update(client);
        }
    }
}
