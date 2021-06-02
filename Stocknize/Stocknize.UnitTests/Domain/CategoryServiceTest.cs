using AutoMapper;
using Moq;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Domain.Models.Category;
using Stocknize.Domain.Services;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Stocknize.UnitTests.Domain
{
    public class CategoryServiceTest
    {
        private Mock<ICategoryRepository> mockCategoryRepository;
        private Mock<IMapper> mockMapper;
        private Category category;
        private CategoryInputModel categoryInput;
        private CategoryOutputModel categoryOutput;

        public CategoryServiceTest()
        {
            //Models
            category = new Category { Id = Guid.NewGuid(), Description = "Refrigerante" };
            categoryInput = new CategoryInputModel { Description = "Refrigerante" };
            categoryOutput = new CategoryOutputModel { Id = Guid.NewGuid(), Description = "Refrigerante" };

            //Mocks
            mockCategoryRepository = new Mock<ICategoryRepository>();
            mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task CategoryModelIsValid_TryAddCategory_ReturnsSuccess()
        {
            mockCategoryRepository.Setup(x => x.Any(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);
            mockCategoryRepository.Setup(x => x.Add(It.IsAny<Category>(), It.IsAny<CancellationToken>())).ReturnsAsync(category);

            mockMapper.Setup(x => x.Map<Category>(It.IsAny<CategoryInputModel>())).Returns(category);
            mockMapper.Setup(x => x.Map<CategoryOutputModel>(It.IsAny<Category>())).Returns(categoryOutput);

            var domain = new CategoryService(mockMapper.Object, mockCategoryRepository.Object);

            var result = await domain.AddProductType(categoryInput, new CancellationToken());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task CategoryAlreadyExist_TryAddCategory_ReturnsCategoryExisted()
        {
            mockCategoryRepository.Setup(x => x.Any(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
            var domain = new CategoryService(mockMapper.Object, mockCategoryRepository.Object);

            await Assert.ThrowsAsync<Exception>(async () => await domain.AddProductType(categoryInput, new CancellationToken()));
        }
    }
}
