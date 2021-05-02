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
            var product = new Product("Skol lata", 2.39M, ProductType.Beer);

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(product));

            var mockInventoryRepository = new Mock<IInventoryRepository>();
            mockInventoryRepository.Setup(x => x.Add(It.IsAny<Inventory>(), It.IsAny<CancellationToken>()));

            var mockMapper = new Mock<IMapper>();
            var mockMovimentationRepository = new Mock<IMovimentationRepository>();
            var mockUserRepository = new Mock<IUserRepository>();

            var inventoryService = new InventoryService(mockProductRepository.Object, mockInventoryRepository.Object, mockMapper.Object, mockMovimentationRepository.Object, mockUserRepository.Object);

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

            var inventoryService = new InventoryService(mockProductRepository.Object, mockInventoryRepository.Object, mockMapper.Object, mockMovimentationRepository.Object, mockUserRepository.Object);

            //assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await inventoryService.AddInventory(Guid.NewGuid(), 10, new CancellationToken()));
        }

        [Fact]
        public async Task MovimentationModelIsValid_MovimentationHasBeenInsertedAndInventoryUpdated_ReturnsMovimentationOutputAsResultMessage()
        {
            //arrange
            var product = new Product("Skol lata", 2.39M, ProductType.Beer);
            var user = new User("Vinicius Santana", "11155863437", new Credentials());
            var movimentation = new Movimentation(MovimentationType.Buy, 100, product, user);
            var movimentationInput = new MovimentationInputModel(Guid.NewGuid(), Guid.NewGuid(), MovimentationType.Buy, 100);
            var movimentationOutput = new MovimentationOutputModel(Guid.NewGuid(), "Vinicius Santana", "Skol lata", "Cerveja", 100);
            var inventory = new Inventory(product, 100);

            var mockInventoryRepository = new Mock<IInventoryRepository>();
            mockInventoryRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Inventory, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(inventory));
            mockInventoryRepository.Setup(x => x.Update(It.IsAny<Inventory>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(inventory));

            var mockMovimentationRepository = new Mock<IMovimentationRepository>();
            mockMovimentationRepository.Setup(x => x.Add(It.IsAny<Movimentation>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(movimentation));

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(product));
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.Get(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(user));

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Movimentation>(It.IsAny<MovimentationInputModel>())).Returns(movimentation);
            mockMapper.Setup(x => x.Map<MovimentationOutputModel>(It.IsAny<Movimentation>())).Returns(movimentationOutput);

            var inventoryService = new InventoryService(mockProductRepository.Object, mockInventoryRepository.Object, mockMapper.Object, mockMovimentationRepository.Object, mockUserRepository.Object);

            //act
            var result = await inventoryService.AddMovimentation(movimentationInput, new CancellationToken());

            //assert

            Assert.NotNull(result);
            Assert.Equal(result.Quantity, movimentationOutput.Quantity);
            Assert.Equal(result.Product, movimentationOutput.Product);
            Assert.Equal(result.User, movimentationOutput.User);
        }
    }
}
