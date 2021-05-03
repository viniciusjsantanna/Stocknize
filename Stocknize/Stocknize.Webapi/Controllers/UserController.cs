using Microsoft.AspNetCore.Mvc;
using Stocknize.Domain.Interfaces.Domain;
using Stocknize.Domain.Models.User;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Stocknize.Domain.Interfaces.Repositories;
using System;
using System.Transactions;


namespace Stocknize.Webapi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserInputModel model, CancellationToken cancellationToken)
        {
            return Ok(await userService.AddUser(model, cancellationToken));
        }

        [HttpGet]
        [Route("getById")]
        public async Task<UserLoggedOutputModel> GetLoggedUser([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            return mapper.Map<UserLoggedOutputModel>(await userRepository.Get(e => e.Id.Equals(id), cancellationToken));
        }

        public async Task<UserLoggedOutputModel> Put([FromQuery] Guid id, [FromBody] UserInputModel productModel, CancellationToken cancellationToken)
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
        [Route("Auth")]
        public async Task<IActionResult> Authenticate(CredentialsInputModel model, CancellationToken cancellationToken)
        {
            return Ok(await userService.Authenticate(model, cancellationToken));
        }
    }
}
