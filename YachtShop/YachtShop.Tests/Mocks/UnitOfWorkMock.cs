using System;
using System.Threading.Tasks;
using YachtShop.Data.UnitOfWork.Abstraction;

namespace YachtShop.Tests.Mocks
{
    public class UnitOfWorkMock : IUnitOfWork
    {
        public async Task SaveChanges()
        {
            return ;
        }
    }
}
