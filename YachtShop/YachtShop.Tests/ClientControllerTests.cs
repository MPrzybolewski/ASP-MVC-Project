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
        public async Task Controller_ShouldReturnIndexView()
        {
            var models = new List<Client>
            {
                new Client(),
                new Client()
            };

            var repositoryMock = new Mock<IClientRepository>();
            repositoryMock.Setup(x => x.GetAll()).ReturnsAsync(models);

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var controller = new ClientController(repositoryMock.Object, unitOfWorkMock.Object);
            var result = await controller.Index();
            var viewResult = (ViewResult)result;
            Assert.Equal("Index", viewResult.ViewName);

        }

        [Fact]
        public async Task Index_Give2ModelsShouldReturnCorrectNumberOfModels()
        {
            var models = new List<Client>
            {
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

            Assert.Equal(2, model.Count);
        }


        [Fact]
        public async Task Index_GiveZeroModelsShouldReturnEmptyModel()
        {
            // Arrange
            var repositoryMock = new Mock<IClientRepository>();
            repositoryMock.Setup(x => x.GetAll()).ReturnsAsync(new List<Client>());

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var controller = new ClientController(repositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = (ViewResult)result;
            var model = (IList<Client>)viewResult.Model;

            Assert.Empty(model);
        }

        [Fact]
        public async Task Detail_GiveExistingClientIdRetunCorrectModel()
        {
            Client client1 = new Client
            {
                FirstName = "Jan"
            };

            Client client2 = new Client
            {
                FirstName = "Staś"
            };

            var expectedFirstName = "Jan";

            var repositoryMock = new Mock<IClientRepository>();
            repositoryMock.Setup(x => x.GetById("1")).ReturnsAsync(client1);
            repositoryMock.Setup(x => x.GetById("2")).ReturnsAsync(client2);

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var controller = new ClientController(repositoryMock.Object, unitOfWorkMock.Object);

            var result = await controller.Details("1") as ViewResult;

            var model = (Client)result.Model;

            Assert.Equal(expectedFirstName, model.FirstName);
        }

        [Fact]
        public async Task Detail_GiveNotExistingClientIdRetunNotFoundView()
        {
            Client client1 = new Client
            {
                FirstName = "Jan"
            };

            Client client2 = new Client
            {
                FirstName = "Staś"
            };

            var repositoryMock = new Mock<IClientRepository>();
            repositoryMock.Setup(x => x.GetById("1")).ReturnsAsync(client1);
            repositoryMock.Setup(x => x.GetById("2")).ReturnsAsync(client2);

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var controller = new ClientController(repositoryMock.Object, unitOfWorkMock.Object);

            var result = await controller.Details("4") as ViewResult;

            Assert.Equal("NotFound", result.ViewName);
        }

        [Fact]
        public async Task Detail_GiveNullClientReturnNotFoundView()
        {
            var repositoryMock = new Mock<IClientRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var controller = new ClientController(repositoryMock.Object, unitOfWorkMock.Object);

            var result = await controller.Details(null) as ViewResult;

            Assert.Equal("NotFound", result.ViewName);
        }
    }
}
