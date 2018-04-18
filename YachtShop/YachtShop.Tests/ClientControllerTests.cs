using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using YachtShop.Controllers;
using YachtShop.Data.Repositories.Interfaces;
using YachtShop.Data.UnitOfWork.Abstraction;
using YachtShop.Models;

namespace YachtShop.Tests
{
    public class ClientControllerTests
    {

        [Fact]
        public async Task Index_GivenThreeResultsShouldReutrnCorrectNumberOfModels()
        {
            var models = new List<Client>
            {
                new Client(),
                new Client(),
                new Client()
            };

            var repositoryMock = new Mock<IClientRepository>();
            repositoryMock.Setup(x => x.GetAll()).ReturnsAsync(models);

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var controller = new ClientController(repositoryMock.Object, unitOfWorkMock.Object);

            var result = await controller.Index();

            var viewResult = (ViewResult)result;
            var model = (IList<Client>)viewResult.Model;

            Assert.Equal(3, model.Count);
        }
    }
}
