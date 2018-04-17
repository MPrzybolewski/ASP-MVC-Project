using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YachtShop.Models;

namespace YachtShop.Data.Repositories.Interfaces
{
    public interface IPurchaseRepository
    {
        Task<IEnumerable<Purchase>> GetAll();

        Task<Purchase> GetById(string id);

        void Add(Purchase purchase);

        void Update(Purchase purchase);

        void Delete(Purchase purchase);
    }
}
