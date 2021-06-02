using AutoMapper;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Exceptions;
using Stocknize.Domain.Interfaces.Domain;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Domain.Models.User;
using System;
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
        public async Task<UserOutputModel> AddUser(UserInputModel userModel, CancellationToken cancellationToken)
        {
            var userExist = await userRepository.Any(e => e.Credentials.Login.Equals(userModel.Login) || e.Cpf.Equals(userModel.Cpf), cancellationToken);

            if (userExist)
            {
                throw new ExistException("O usuário existente!");
            }

            var user = mapper.Map<User>(userModel);
            user.Company = await companyRepository.Get(e => e.Id.Equals(userModel.CompanyId), cancellationToken);

            if (user.Company is null)
            {
                throw new NotFoundException("Não foi possível encontrar a empresa informada!");
            }

            return mapper.Map<UserOutputModel>(await userRepository.Add(user, cancellationToken));
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

        public async Task DeleteUser(Guid userId, CancellationToken cancellationToken)
        {
            var user = await userRepository.Get(e => e.Id.Equals(userId), cancellationToken)
                ?? throw new NotFoundException("Não foi possível encontrar o usuário");

            await userRepository.Delete(user, cancellationToken);
        }

        public async Task<UserOutputModel> UpdateUser(Guid userId, UserInputModel userModel, CancellationToken cancellationToken)
        {
            var user = await userRepository.Get(e => e.Id.Equals(userId), cancellationToken)
                            ?? throw new NotFoundException("Não foi possível encontrar o usuário");

            mapper.Map(userModel, user);

            var result = await userRepository.Update(user, cancellationToken);

            return mapper.Map<UserOutputModel>(result);
        }
    }
}
