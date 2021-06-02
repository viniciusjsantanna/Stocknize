using AutoMapper;
using Moq;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Domain.Models.Company;
using Stocknize.Domain.Services;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Stocknize.UnitTests.Domain
{
    public class CompanyServiceTest
    {
        private Company company;
        private CompanyInputModel companyInput;
        private CompanyOutputModel companyOutput;
        private CancellationToken cancellationToken;
        private Mock<IMapper> mockMapper;
        private Mock<ICompanyRepository> mockCompanyRepository;

        public CompanyServiceTest()
        {
            //Models
            company = new Company("League of Legends");
            companyInput = new CompanyInputModel { Name = "League of Legends" };
            companyOutput = new CompanyOutputModel { Id = Guid.NewGuid(), Name = "League of Legends" };
            cancellationToken = new CancellationToken();
            //Mocks
            mockMapper = new Mock<IMapper>();
            mockCompanyRepository = new Mock<ICompanyRepository>();
        }
        [Fact]
        public async Task CompanyInputModelIsValid_WhenTryAddACompany_ReturnsSuccess()
        {
            mockMapper.Setup(x => x.Map<Company>(It.IsAny<CompanyInputModel>())).Returns(company);
            mockMapper.Setup(x => x.Map<CompanyOutputModel>(It.IsAny<Company>())).Returns(companyOutput);

            mockCompanyRepository.Setup(x => x.Any(It.IsAny<Expression<Func<Company, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);
            mockCompanyRepository.Setup(x => x.Add(It.IsAny<Company>(), It.IsAny<CancellationToken>())).ReturnsAsync(company);

            var domain = GenerateCompanyService();

            var result = await domain.AddCompany(companyInput, cancellationToken);

            Assert.NotNull(result);
        }

        private CompanyService GenerateCompanyService()
        {
            return new CompanyService(mockCompanyRepository.Object, mockMapper.Object);
        }

        [Fact]
        public async Task CompanyNameIsAlreadyInserted_WhenTryAddACompany_ReturnsCompanyAlreadyInsertedMessage()
        {
            mockCompanyRepository.Setup(x => x.Any(It.IsAny<Expression<Func<Company, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var domain = GenerateCompanyService();

            await Assert.ThrowsAsync<Exception>(async () => await domain.AddCompany(companyInput, cancellationToken));
        }
    }
}
