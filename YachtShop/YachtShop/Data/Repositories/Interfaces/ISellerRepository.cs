using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YachtShop.Models;

namespace YachtShop.Data.Repositories.Interfaces
{
    public interface ISellerRepository
    {
        Task<IEnumerable<Seller>> GetAll();

        Task<Seller> GetById(string id);

        void Add(Seller seller);

        void Update(Seller seller);

        void Delete(Seller seller);
    }
}
