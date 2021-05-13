using AutoMapper;
using Moq;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Enums;
using Stocknize.Domain.Interfaces.Domain;
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
    public class MovimentationServiceTest
    {
        [Fact]
        public async Task MovimentationModelIsValid_MovimentationHasBeenInsertedAndInventoryUpdated_ReturnsMovimentationOutputAsResultMessage()
        {
            //arrange
            var product = new Product("Skol lata", 2.39M);
            var user = new User("Vinicius Santana", "11155863437", new Credentials());
            var movimentation = new Movimentation(MovimentationType.Buy, 100, product, user);
            var movimentationInput = new MovimentationInputModel(Guid.NewGuid(), MovimentationType.Buy, 100, 2.39M)
            {
                UserId = Guid.NewGuid()
            };
            var movimentationOutput = new MovimentationOutputModel(Guid.NewGuid(), "Vinicius Santana", "Skol lata", 100)
            {
                Type = "Cerveja"
            };
            var inventory = new Inventory(product, 100);

            var mockInventoryService = new Mock<IInventoryService>();
            mockInventoryService.Setup(x => x.UpdateInventory(It.IsAny<MovimentationInputModel>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var mockMovimentationRepository = new Mock<IMovimentationRepository>();
            mockMovimentationRepository.Setup(x => x.Add(It.IsAny<Movimentation>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(movimentation));

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(product));

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.Get(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(user));

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Movimentation>(It.IsAny<MovimentationInputModel>())).Returns(movimentation);
            mockMapper.Setup(x => x.Map<MovimentationOutputModel>(It.IsAny<Movimentation>())).Returns(movimentationOutput);

            var inventoryService = new MovimentationService(mockProductRepository.Object, mockUserRepository.Object, mockMovimentationRepository.Object,
                mockInventoryService.Object, mockMapper.Object);

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
