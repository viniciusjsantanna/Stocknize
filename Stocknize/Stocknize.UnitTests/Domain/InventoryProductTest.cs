using AutoMapper;
using Moq;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Enums;
using Stocknize.Domain.Exceptions;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Domain.Models.Inventories;
using Stocknize.Domain.Services;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Stocknize.UnitTests.Domain
{
    public class InventoryProductTest
    {
        [Fact]
        public async Task InventoryModelIsValidAfterProductInsertedAtDb_InventoryInsertedSucessfully_ReturnsTaskCompleteAsResultMessage()
        {
            //arrange
            var product = new Product("Skol lata", 2.39M);

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(product));

            var mockInventoryRepository = new Mock<IInventoryRepository>();
            mockInventoryRepository.Setup(x => x.Add(It.IsAny<Inventory>(), It.IsAny<CancellationToken>()));

            var mockMapper = new Mock<IMapper>();
            var mockMovimentationRepository = new Mock<IMovimentationRepository>();
            var mockUserRepository = new Mock<IUserRepository>();

            var inventoryService = new InventoryService(mockProductRepository.Object, mockInventoryRepository.Object);

            //act
            await inventoryService.AddInventory(Guid.NewGuid(), 10, new CancellationToken());

        }

        [Fact]
        public async Task InventoryModelIsInvalidAfterProductNotInsertedAtDb_GottenNoProduct_ThrowsANotFoundException()
        {
            //arrange
            Product product = null;

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(product));

            var mockInventoryRepository = new Mock<IInventoryRepository>();
            var mockMapper = new Mock<IMapper>();
            var mockMovimentationRepository = new Mock<IMovimentationRepository>();
            var mockUserRepository = new Mock<IUserRepository>();

            var inventoryService = new InventoryService(mockProductRepository.Object, mockInventoryRepository.Object);

            //assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await inventoryService.AddInventory(Guid.NewGuid(), 10, new CancellationToken()));
        }
    }
}
