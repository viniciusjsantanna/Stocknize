using Microsoft.AspNetCore.Mvc;
using Stocknize.Domain.Interfaces.Domain;
using Stocknize.Domain.Models.User;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Stocknize.Domain.Interfaces.Repositories;
using System;
using System.Transactions;
using Stocknize.Crosscutting.Extensions;

namespace Stocknize.Webapi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserController(IUserService userService, IUserRepository userRepository, IMapper mapper)
        {
            this.userService = userService;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<UserOutputModel> Post([FromBody] UserInputModel model, CancellationToken cancellationToken)
        {
            return await userService.AddUser(model, cancellationToken);
        }

        [HttpGet]
        [Route("getLoggedUser")]
        public async Task<UserInputModel> GetLoggedUser(CancellationToken cancellationToken)
        {
            return mapper.Map<UserInputModel>(await userRepository.Get(e => e.Id.Equals(HttpContext.GetLoggedUserId()), cancellationToken));
        }

        [HttpPut]
        public async Task<UserOutputModel> Put([FromQuery] Guid id, [FromBody] UserInputModel productModel, CancellationToken cancellationToken)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var result = await userService.UpdateUser(id, productModel, cancellationToken);
            transaction.Complete();

            return result;
        }

        [HttpDelete]
        public async Task Delete([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            await userService.DeleteUser(id, cancellationToken);
            transaction.Complete();
        }

        [HttpPost]
        [Route("auth")]
        public async Task<IActionResult> Authenticate(CredentialsInputModel model, CancellationToken cancellationToken)
        {
            return Ok(await userService.Authenticate(model, cancellationToken));
        }
    }
}
