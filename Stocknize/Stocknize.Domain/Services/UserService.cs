using AutoMapper;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Exceptions;
using Stocknize.Domain.Interfaces.Domain;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Domain.Models.User;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly ICompanyRepository companyRepository;
        private readonly IMapper mapper;
        private readonly IJwtGenerator jwtGenerator;

        public UserService(IUserRepository userRepository, ICompanyRepository companyRepository, IMapper mapper, IJwtGenerator jwtGenerator)
        {
            this.userRepository = userRepository;
            this.companyRepository = companyRepository;
            this.mapper = mapper;
            this.jwtGenerator = jwtGenerator;
        }
        public async Task<User> AddUser(UserInputModel userModel, CancellationToken cancellationToken)
        {
            var userExist = await userRepository.Any(e => e.Credentials.Login.Equals(userModel.Login), cancellationToken);

            if (userExist)
            {
                throw new System.Exception("O usuário existente!");
            }
            var user = mapper.Map<User>(userModel);
            user.Company = await companyRepository.Get(e => e.Id.Equals(userModel.CompanyId), cancellationToken);

            return await userRepository.Add(user, cancellationToken);
        }

        public async Task<UserLoggedOutputModel> Authenticate(CredentialsInputModel model, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserWithCompany(model.Login, cancellationToken)
                ?? throw new NotFoundException("Login e Senha invalidos");

            if (user.Credentials is not null && !user.Credentials.isAuthentic(model.Password))
            {
                throw new NotFoundException("Login e Senha invalidos");
            }

            var userLogged = mapper.Map<UserLoggedOutputModel>(user);
            userLogged.JsonWebToken = await jwtGenerator.GenerateToken(user);

            return userLogged;
        }
    }
}
