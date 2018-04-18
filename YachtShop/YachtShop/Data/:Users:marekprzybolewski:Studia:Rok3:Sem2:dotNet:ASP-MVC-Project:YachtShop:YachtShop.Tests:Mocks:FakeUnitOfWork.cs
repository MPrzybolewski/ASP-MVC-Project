using System;
using System.Threading.Tasks;
using YachtShop.Data.UnitOfWork.Abstraction;

namespace YachtShop.Tests.Mocks
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        public Task SaveChanges()
        {
            return null;
        }
    }
}
