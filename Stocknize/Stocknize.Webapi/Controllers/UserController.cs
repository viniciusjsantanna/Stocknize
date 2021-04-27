using Microsoft.AspNetCore.Mvc;
using Stocknize.Domain.Interfaces.Domain;
using Stocknize.Domain.Models.User;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Webapi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserInputModel model, CancellationToken cancellationToken)
        {
            return Ok(await userService.AddUser(model, cancellationToken));
        }

        [HttpPost]
        [Route("Auth")]
        public async Task<IActionResult> Authenticate(CredentialsInputModel model, CancellationToken cancellationToken)
        {
            return Ok(await userService.Authenticate(model, cancellationToken));
        }
    }
}
