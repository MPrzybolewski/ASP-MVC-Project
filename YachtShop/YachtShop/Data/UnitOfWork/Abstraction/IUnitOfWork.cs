using System;
using System.Threading.Tasks;

namespace YachtShop.Data.UnitOfWork.Abstraction
{
    public interface IUnitOfWork
    {
        Task SaveChanges();
    }
}
