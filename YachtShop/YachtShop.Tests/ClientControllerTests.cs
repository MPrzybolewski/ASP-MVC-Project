using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using YachtShop.Controllers;
using YachtShop.Data.Repositories.Interfaces;
using YachtShop.Data.UnitOfWork.Abstraction;
using YachtShop.Models;
using YachtShop.Tests.Mocks;

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
        public async Task Controller_ShouldReturnDetailsView()
        {
            Client client1 = new Client
            {
                FirstName = "Jan"
            };

            var repositoryMock = new Mock<IClientRepository>();
            repositoryMock.Setup(x => x.GetById("1")).ReturnsAsync(client1);

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var controller = new ClientController(repositoryMock.Object, unitOfWorkMock.Object);
            var result = await controller.Details("1");
            var viewResult = (ViewResult)result;
            Assert.Equal("Details", viewResult.ViewName);
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


        [Fact]
        public void Controller_ShouldReturnCreateView()
        {
            var repositoryMock = new Mock<IClientRepository>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var controller = new ClientController(repositoryMock.Object, unitOfWorkMock.Object);
            var result =  controller.Create();
            var viewResult = (ViewResult)result;
            Assert.Equal("Create", viewResult.ViewName);
        }

        [Fact]
        public async Task Create_CorrectRedirectToIndex()
        {
            Client client1 = new Client();

            var repositoryMock = new Mock<IClientRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var controller = new ClientController(repositoryMock.Object, unitOfWorkMock.Object);

            var result = await controller.Create(client1);
            var redirectResult = (RedirectToActionResult)result;

            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public async Task Create_CorrectAdding()
        {
            Client client1 = new Client();

            var repositoryMock = new ClientRepositoryMock();
            var unitOfWorkMock = new UnitOfWorkMock();

            var controller = new ClientController(repositoryMock, unitOfWorkMock);

            await controller.Create(client1);

            IEnumerable<Client> clientsList = await repositoryMock.GetAll();
            int result = clientsList.Count();

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Controller_ShouldReturnEditView()
        {
            Client client1 = new Client();

            var repositoryMock = new Mock<IClientRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            repositoryMock.Setup(x => x.GetById("1")).ReturnsAsync(client1);

            var controller = new ClientController(repositoryMock.Object, unitOfWorkMock.Object);
            var result = controller.Edit("1");
            var viewResult = await result as ViewResult;
            Assert.Equal("Edit", viewResult.ViewName);
        }

        [Fact]
        public async Task Edit_GiveDiffrentIdAndClientReturnNotFoundView()
        {
            Client client1 = new Client
            {
                ClientId = "1"
            };

            var repositoryMock = new Mock<IClientRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var controller = new ClientController(repositoryMock.Object, unitOfWorkMock.Object);
            var result = controller.Edit("2", client1);
            var viewResult = await result as ViewResult;
            Assert.Equal("NotFound", viewResult.ViewName);
        }

        [Fact]
        public async Task Edit_CorrectRedirectToIndex()
        {
            Client client1 = new Client
            {
                ClientId = "1"
            };

            var repositoryMock = new Mock<IClientRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var controller = new ClientController(repositoryMock.Object, unitOfWorkMock.Object);

            var result = await controller.Edit("1", client1);
            var redirectResult = (RedirectToActionResult)result;

            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public async Task Edit_CorrectUpdate()
        {
            Client client1 = new Client
            {
                FirstName = "Jan"
            };

            var repositoryMock = new ClientRepositoryMock();
            var unitOfWorkMock = new UnitOfWorkMock();

            var controller = new ClientController(repositoryMock, unitOfWorkMock);

            await controller.Create(client1);

            client1.FirstName = "Staś";
            await controller.Edit(client1.ClientId, client1);

            Client clientResult = await repositoryMock.GetById(client1.ClientId);
            string result = clientResult.FirstName;

            Assert.Equal("Staś", result);
        }

        [Fact]
        public async Task Edit_UpdateNonExistingClientThrowException()
        {
            Client client1 = new Client
            {
                FirstName = "Jan"
            };

            var repositoryMock = new ClientRepositoryMock();
            var unitOfWorkMock = new UnitOfWorkMock();

            var controller = new ClientController(repositoryMock, unitOfWorkMock);

            client1.FirstName = "Staś";

            await Assert.ThrowsAsync<Exception>(() => controller.Edit(client1.ClientId, client1));
        }


    }
}
