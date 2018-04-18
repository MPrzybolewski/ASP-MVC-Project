using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YachtShop.Models;

namespace YachtShop.Data.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAll();

        Task<Client> GetById(string id);

        void Add(Client client);

        void Update(Client client);

        void Delete(Client client);
    }
}
