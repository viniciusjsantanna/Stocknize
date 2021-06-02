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
        private readonly Mock<IProductRepository> mockProductRepository;
        private readonly Mock<IInventoryService> mockInventoryService;
        private readonly Mock<ICompanyRepository> mockUserService;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<ICategoryRepository> mockProductTypeRepository;

        public ProductDomainTest()
        {
            mockProductRepository = new Mock<IProductRepository>();
            mockInventoryService = new Mock<IInventoryService>();
            mockUserService = new Mock<ICompanyRepository>();
            mockMapper = new Mock<IMapper>();
            mockProductTypeRepository = new Mock<ICategoryRepository>();
        }

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

            mockProductRepository.Setup(x => x.Any(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(false));
            mockProductRepository.Setup(x => x.Add(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(product));

            mockMapper.Setup(x => x.Map<Product>(It.IsAny<ProductInputModel>())).Returns(product);
            mockMapper.Setup(x => x.Map<ProductOutputModel>(It.IsAny<Product>())).Returns(productOutput);

            mockUserService.Setup(x => x.Get(It.IsAny<Expression<Func<Company, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(company));

            var productService = GenerateProductServiceIntance();
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

            mockProductRepository.Setup(x => x.Any(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));
            mockUserService.Setup(x => x.Get(It.IsAny<Expression<Func<Company, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(company));

            var productService = GenerateProductServiceIntance();

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

            mockMapper.Setup(x => x.Map(It.IsAny<ProductInputModel>(), It.IsAny<Product>())).Returns(product);
            mockMapper.Setup(x => x.Map<ProductOutputModel>(It.IsAny<Product>())).Returns(productOutput);

            mockProductRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(productDb);
            mockProductRepository.Setup(x => x.Update(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

            mockUserService.Setup(x => x.Get(It.IsAny<Expression<Func<Company, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(company);

            var productService = GenerateProductServiceIntance();

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

            mockProductRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(product));
            mockUserService.Setup(x => x.Get(It.IsAny<Expression<Func<Company, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(company));

            var productService = GenerateProductServiceIntance();

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

            mockProductRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(product));
            mockProductRepository.Setup(x => x.Delete(It.IsAny<Product>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var productService = GenerateProductServiceIntance();

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

            mockProductRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(product));
            mockProductRepository.Setup(x => x.Delete(It.IsAny<Product>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            ProductService productService = GenerateProductServiceIntance();

            //act
            await Assert.ThrowsAsync<NotFoundException>(async () => await productService.DeleteProduct(Guid.NewGuid(), new CancellationToken()));
        }

        private ProductService GenerateProductServiceIntance()
        {
            return new ProductService(mockProductRepository.Object, mockInventoryService.Object, mockUserService.Object, mockMapper.Object, mockProductTypeRepository.Object);
        }
    }
}
