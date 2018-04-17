using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YachtShop.Models;

namespace YachtShop.Data.Repositories.Interfaces
{
    public interface IYachtRepository
    {
        Task<IEnumerable<Yacht>> GetAll();

        Task<Yacht> GetById(string id);

        void Add(Yacht yacht);

        void Update(Yacht yacht);

        void Delete(Yacht yacht);
    }
}
