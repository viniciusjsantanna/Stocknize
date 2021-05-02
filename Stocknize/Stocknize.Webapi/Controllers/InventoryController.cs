using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stocknize.Domain.Interfaces.Domain;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Domain.Models.Inventories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Webapi.Controllers
{
    [Route("api/inventories")]
    [ApiController]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository inventoryRepository;
        private readonly IInventoryService inventoryService;
        private readonly IMapper mapper;

        public InventoryController(IInventoryRepository inventoryRepository, IInventoryService inventoryService, IMapper mapper)
        {
            this.inventoryRepository = inventoryRepository;
            this.inventoryService = inventoryService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IList<InventoryOutputModel>> Get(CancellationToken cancellationToken)
        {
            return mapper.Map<IList<InventoryOutputModel>>(await inventoryRepository.GetInventories(cancellationToken));
        }

        [HttpPost]
        public async Task<MovimentationOutputModel> Post([FromBody] MovimentationInputModel movimentationInputModel, CancellationToken cancellationToken)
        {
            movimentationInputModel.UserId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(e => e.Type.Contains("nameidentifier")).Value);
            return await inventoryService.AddMovimentation(movimentationInputModel, cancellationToken);
        }

        [HttpGet]
        [Route("movimentations")]
        public async Task<IList<MovimentationOutputModel>> GetMovimentations([FromServices] IMovimentationRepository movimentationRepository, CancellationToken cancellationToken)
        {
            return mapper.Map<IList<MovimentationOutputModel>>(await movimentationRepository.GetMovimentations(cancellationToken));
        }
    }
}
