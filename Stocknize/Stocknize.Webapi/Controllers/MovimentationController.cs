using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stocknize.Crosscutting.Extensions;
using Stocknize.Domain.Interfaces.Domain;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Domain.Models.Inventories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentationController : ControllerBase
    {
        private readonly IMovimentationService movimentationService;
        private readonly IMapper mapper;

        public MovimentationController(IMovimentationService movimentationService, IMapper mapper)
        {
            this.movimentationService = movimentationService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<MovimentationOutputModel> Post([FromBody] MovimentationInputModel movimentationInputModel, CancellationToken cancellationToken)
        {
            movimentationInputModel.UserId = HttpContext.GetLoggedUserId();
            var result = await movimentationService.AddMovimentation(movimentationInputModel, cancellationToken);
            return result;
        }

        [HttpGet]
        public async Task<IList<MovimentationOutputModel>> GetMovimentations([FromServices] IMovimentationRepository movimentationRepository, CancellationToken cancellationToken)
        {
            return mapper.Map<IList<MovimentationOutputModel>>(await movimentationRepository.GetMovimentations(cancellationToken));
        }
    }
}
