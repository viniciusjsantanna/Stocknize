using AutoMapper;
using Moq;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Exceptions;
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
            var product = new Product("Skol lata", 2.39M);
            var company = new Company("Vendinha da esquina");
            var productInputModel = new ProductInputModel("Skol lata", 2.39M, Guid.NewGuid())
            {
                CompanyId = Guid.NewGuid()
            };

            var productOutput = new ProductOutputModel(Guid.NewGuid(), "Skol lata", 2.39M)
            {
                Type = "Cerveja"
            };

            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.Any(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(false));
            mockRepository.Setup(x => x.Add(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(product));

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Product>(It.IsAny<ProductInputModel>())).Returns(product);
            mockMapper.Setup(x => x.Map<ProductOutputModel>(It.IsAny<Product>())).Returns(productOutput);

            var mockInventoryService = new Mock<IInventoryService>();
            var mockUserService = new Mock<ICompanyRepository>();
            mockUserService.Setup(x => x.Get(It.IsAny<Expression<Func<Company, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(company));

            var productService = new ProductService(mockRepository.Object, mockInventoryService.Object, mockUserService.Object, mockMapper.Object);

            //act

            var result = await productService.AddProduct(productInputModel, new CancellationToken());

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.Name, productOutput.Name);
            Assert.Equal(result.Price, productOutput.Price);
            Assert.True(result.Type.Equals("Cerveja"));
        }

        [Fact]
        public async Task ProductInputModelIsValidAndProductExistAtDb_ProductNotAdded_ReturnsAlreadyExistExceptionError()
        {
            //arrange
            var product = new Product("Skol lata", 2.39M);
            var company = new Company("Vendinha da esquina");
            var productInputModel = new ProductInputModel("Skol lata", 2.39M, Guid.NewGuid())
            {
                CompanyId = Guid.NewGuid()
            };

            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.Any(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));

            var mockMapper = new Mock<IMapper>();
            var mockInventoryService = new Mock<IInventoryService>();

            var mockUserService = new Mock<ICompanyRepository>();
            mockUserService.Setup(x => x.Get(It.IsAny<Expression<Func<Company, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(company));

            var productService = new ProductService(mockRepository.Object, mockInventoryService.Object, mockUserService.Object, mockMapper.Object);

            //assert
            await Assert.ThrowsAsync<Exception>(async () => await productService.AddProduct(productInputModel, new CancellationToken()));
        }

        [Fact]
        public async Task ProductIntpuModelIsValidAndProductExistAtDb_ProductUpdated_ReturnsProductUpdatedAsSuccessMessage()
        {
            //arrange
            var productDb = new Product("Skol lata", 2.39M)
            {
                Id = Guid.NewGuid()
            };
            var company = new Company("Vendinha da esquina");

            var product = new Product("Skol latão", 2.50M)
            {
                Id = Guid.NewGuid()
            };

            var productInputModel = new ProductInputModel("Skol lata", 2.39M, Guid.NewGuid())
            {
                CompanyId = Guid.NewGuid()
            };

            var productOutput = new ProductOutputModel(Guid.NewGuid(), "Skol lata", 2.39M)
            {
                Type = "Cerveja"
            };

            var mockInventoryService = new Mock<IInventoryService>();

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map(It.IsAny<ProductInputModel>(), It.IsAny<Product>())).Returns(product);
            mockMapper.Setup(x => x.Map<ProductOutputModel>(It.IsAny<Product>())).Returns(productOutput);

            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(productDb));
            mockRepository.Setup(x => x.Update(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(product));

            var mockUserService = new Mock<ICompanyRepository>();
            mockUserService.Setup(x => x.Get(It.IsAny<Expression<Func<Company, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(company));

            var productService = new ProductService(mockRepository.Object, mockInventoryService.Object, mockUserService.Object, mockMapper.Object);

            //Act

            var result = await productService.UpdateProduct(Guid.NewGuid(), productInputModel, new CancellationToken());

            //assert

            Assert.NotNull(result);
            Assert.Equal(result.Name, productOutput.Name);
            Assert.Equal(result.Price, productOutput.Price);
            Assert.Equal(result.Type, productOutput.Type);
        }

        [Fact]
        public async Task ProductUpdateModelIsValid_TryToGetProductButReturnsNull_ReturnsNotFoundExceptionAsResultMessage()
        {
            //arrange
            Product product = null;
            var company = new Company("Vendinha da esquina");
            var productInputModel = new ProductInputModel("Skol lata", 2.39M, Guid.NewGuid())
            {
                CompanyId = Guid.NewGuid()
            };
            var mockInventoryService = new Mock<IInventoryService>();

            var mockMapper = new Mock<IMapper>();

            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(product));
            var mockUserService = new Mock<ICompanyRepository>();
            mockUserService.Setup(x => x.Get(It.IsAny<Expression<Func<Company, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(company));

            var productService = new ProductService(mockRepository.Object, mockInventoryService.Object, mockUserService.Object, mockMapper.Object);

            //assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await productService.UpdateProduct(Guid.NewGuid(), productInputModel, new CancellationToken()));
        }

        [Fact]
        public async Task ProductDeleteModelIsValid_DeleteProduct_ReturnsTaskComplete()
        {
            //arrange
            var product = new Product("Skol latão", 2.50M)
            {
                Id = Guid.NewGuid()
            };

            var mockInventoryService = new Mock<IInventoryService>();

            var mockMapper = new Mock<IMapper>();

            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(product));
            mockRepository.Setup(x => x.Delete(It.IsAny<Product>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            var mockUserService = new Mock<ICompanyRepository>();

            var productService = new ProductService(mockRepository.Object, mockInventoryService.Object, mockUserService.Object, mockMapper.Object);

            //act
            await productService.DeleteProduct(Guid.NewGuid(), new CancellationToken());
        }

        [Fact]
        public async Task ProductDeleteModelIsNotValid_GottenNoProduct_ReturnsThrowNotFoundException()
        {
            //arrange
            Product product = null;
            var mockInventoryService = new Mock<IInventoryService>();

            var mockMapper = new Mock<IMapper>();

            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(product));
            mockRepository.Setup(x => x.Delete(It.IsAny<Product>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            var mockUserService = new Mock<ICompanyRepository>();

            var productService = new ProductService(mockRepository.Object, mockInventoryService.Object, mockUserService.Object, mockMapper.Object);

            //act
            await Assert.ThrowsAsync<NotFoundException>(async () => await productService.DeleteProduct(Guid.NewGuid(), new CancellationToken()));
        }
    }
}
