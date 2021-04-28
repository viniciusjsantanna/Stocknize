using AutoMapper;
using Moq;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Enums;
using Stocknize.Domain.Interfaces.Domain;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Domain.Models.Products;
using Stocknize.Domain.Services;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Stocknize.UnitTests.Domain
{
    public class ProductDomainTest
    {
        [Fact]
        public async Task ProductInputModelIsValidAndProductNotExistAtDb_ProductAdded_ReturnsProductAddedAtSuccessMessage()
        {
            //arrange
            var product = new Product("Skol lata", 2.39M, ProductType.Beer);
            var productInputModel = new ProductInputModel
            {
                Name = "Skol lata",
                Price = 2.39M,
                Type = ProductType.Beer
            };

            var productOutput = new ProductOutputModel(Guid.NewGuid(), "Skol lata", 2.39M, "Cerveja");

            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.Any(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(false));
            mockRepository.Setup(x => x.Add(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(product));

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Product>(It.IsAny<ProductInputModel>())).Returns(product);
            mockMapper.Setup(x => x.Map<ProductOutputModel>(It.IsAny<Product>())).Returns(productOutput);

            var mockInventoryService = new Mock<IInventoryService>();

            var productService = new ProductService(mockRepository.Object, mockInventoryService.Object, mockMapper.Object);

            //act

            var result = await productService.AddProduct(productInputModel, new CancellationToken());

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.Name, product.Name);
            Assert.Equal(result.Price, product.Price);
            Assert.True(result.Type.Equals("Cerveja"));
        }

        [Fact]
        public async Task ProductInputModelIsValidAndProductExistAtDb_ProductNotAdded_ReturnsAlreadyExistExceptionError()
        {
            //arrange
            var product = new Product("Skol lata", 2.39M, ProductType.Beer);
            var productInputModel = new ProductInputModel
            {
                Name = "Skol lata",
                Price = 2.39M,
                Type = ProductType.Beer
            };

            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.Any(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(false));

            var mockMapper = new Mock<IMapper>();
            var mockInventoryService = new Mock<IInventoryService>();

            var productService = new ProductService(mockRepository.Object, mockInventoryService.Object, mockMapper.Object);
            //act

            var result = await productService.AddProduct(productInputModel, new CancellationToken());

            //assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ProductIntpuModelIsValidAndProductExistAtDb_ProductUpdated_ReturnsProductUpdatedAsSuccessMessage()
        {

            await Task.CompletedTask;
        }
    }
}
