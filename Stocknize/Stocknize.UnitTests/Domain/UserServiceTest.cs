using AutoMapper;
using Moq;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Exceptions;
using Stocknize.Domain.Interfaces.Domain;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Domain.Models.Jwt;
using Stocknize.Domain.Models.User;
using Stocknize.Domain.Services;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Stocknize.UnitTests.Domain
{
    public class UserServiceTest
    {
        private User user;
        private UserInputModel userInput;
        private UserOutputModel userOutput;
        private Company company;
        private Mock<IUserRepository> mockUserRepository;
        private Mock<ICompanyRepository> mockCompanyRepository;
        private Mock<IJwtGenerator> mockJwtService;
        private Mock<IMapper> mockMapper;

        public UserServiceTest()
        {
            //models
            user = new User("Vinicius Santana", "12345678910", new Credentials { Login = "vinicius.santana", Password = "123456" });
            userInput = new UserInputModel { Name = "Vinicius Santana", Cpf = "12345678910", Password = "123456", Login = "vinicius.santana", CompanyId = Guid.NewGuid() };
            userOutput = new UserOutputModel("Vinicius Santana", "Loja da Esquina", "12345678910");
            company = new Company("Loja da Esquina");

            //mocks
            mockUserRepository = new Mock<IUserRepository>();
            mockCompanyRepository = new Mock<ICompanyRepository>();
            mockJwtService = new Mock<IJwtGenerator>();
            mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task UserInputModelIsValid_TryAddAnUser_ReturnsSuccess()
        {
            mockUserRepository.Setup(x => x.Any(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);
            mockUserRepository.Setup(x => x.Add(It.IsAny<User>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);
            mockCompanyRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Company, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(company);
            mockMapper.Setup(x => x.Map<User>(It.IsAny<UserInputModel>())).Returns(user);
            mockMapper.Setup(x => x.Map<UserOutputModel>(It.IsAny<User>())).Returns(userOutput);

            var domain = new UserService(mockUserRepository.Object, mockCompanyRepository.Object, mockMapper.Object, mockJwtService.Object);

            var result = await domain.AddUser(userInput, new CancellationToken());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task UserIsAlreadyInserted_TryAddAnUser_ReturnsUserIsInserted()
        {
            mockUserRepository.Setup(x => x.Any(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
            var domain = new UserService(mockUserRepository.Object, mockCompanyRepository.Object, mockMapper.Object, mockJwtService.Object);

            await Assert.ThrowsAsync<ExistException>(async () => await domain.AddUser(userInput, new CancellationToken()));
        }

        [Fact]
        public async Task CompanyNotFound_TryAddAnUserAndGetCompany_ReturnsCompanyNotFounded()
        {
            company = null;
            mockUserRepository.Setup(x => x.Any(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);
            mockMapper.Setup(x => x.Map<User>(It.IsAny<UserInputModel>())).Returns(user);
            mockCompanyRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Company, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(company);
            UserService domain = GenerateUserService();

            await Assert.ThrowsAsync<NotFoundException>(async () => await domain.AddUser(userInput, new CancellationToken()));
        }

        private UserService GenerateUserService()
        {
            return new UserService(mockUserRepository.Object, mockCompanyRepository.Object, mockMapper.Object, mockJwtService.Object);
        }

        [Fact]
        public async Task AutheticateModelIsValid_TryAuthenticate_ReturnsUserAuthenticatedAndJwtToken()
        {
            var jwt = new JwtModel { AccessToken = "TOKEN_AUTHETICATED", ExpiresAt = DateTime.Now };
            var model = new CredentialsInputModel { Login = "vinicius.santana", Password = "123456" };
            var userLogged = new UserLoggedOutputModel { JsonWebToken = jwt, Login = "vinicius.santana", Name = "Vinicius Santana" };
            mockUserRepository.Setup(x => x.GetUserWithCompany(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);
            mockJwtService.Setup(x => x.GenerateToken(It.IsAny<User>())).ReturnsAsync(jwt);
            mockMapper.Setup(x => x.Map<UserLoggedOutputModel>(It.IsAny<User>())).Returns(userLogged);

            var domain = GenerateUserService();

            var result = await domain.Authenticate(model, new CancellationToken());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task AuthenticateModelPasswordOrLoginInvalid_TryAuthenticate_returnsNotFoundException()
        {
            var model = new CredentialsInputModel { Login = "vinicius.santana", Password = "123456" };
            user.Credentials.Password = "12345";
            mockUserRepository.Setup(x => x.GetUserWithCompany(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);

            var domain = GenerateUserService();

            await Assert.ThrowsAsync<NotFoundException>(async () => await domain.Authenticate(model, new CancellationToken()));
        }

        [Fact]
        public async Task UserUpdateModelIsValid_TryUpdateAnUser_ReturnsSuccess()
        {
            mockUserRepository.Setup(x => x.Update(It.IsAny<User>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);
            mockUserRepository.Setup(x => x.Get(It.IsAny<Expression<Func<User,bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);
            mockMapper.Setup(x => x.Map(It.IsAny<UserInputModel>(), It.IsAny<User>())).Returns(user);
            mockMapper.Setup(x => x.Map<UserOutputModel>(It.IsAny<User>())).Returns(userOutput);

            var domain = GenerateUserService();

            var result = await domain.UpdateUser(Guid.NewGuid(), userInput, new CancellationToken());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task UserNotFounded_TryUpdateAnUser_ReturnsNotFound()
        {
            user = null;
            mockUserRepository.Setup(x => x.Get(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);

            var domain = GenerateUserService();

            await Assert.ThrowsAsync<NotFoundException>(async () => await domain.UpdateUser(Guid.NewGuid(), userInput, new CancellationToken()));
        }

        [Fact]
        public async Task UserDeleteModelIsValid_TryDeleteAnUser_ReturnsTaskComplete()
        {
            mockUserRepository.Setup(x => x.Get(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);
            mockUserRepository.Setup(x => x.Delete(It.IsAny<User>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var domain = GenerateUserService();

            await domain.DeleteUser(Guid.NewGuid(), new CancellationToken());
        }

        [Fact]
        public async Task UserNotFounded_TryDeleteAnUser_ReturnsNotFound()
        {
            user = null;
            mockUserRepository.Setup(x => x.Get(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);

            var domain = GenerateUserService();

            await Assert.ThrowsAsync<NotFoundException>(async () => await domain.DeleteUser(Guid.NewGuid(), new CancellationToken()));
        }

    }
}
