using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Domain.Models.Inventories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Webapi.Controllers
{
    [Route("api/inventories")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository inventoryRepository;
        private readonly IMapper mapper;

        public InventoryController(IInventoryRepository inventoryRepository, IMapper mapper)
        {
            this.inventoryRepository = inventoryRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IList<InventoryOutputModel>> Get(CancellationToken cancellationToken)
        {
            return mapper.Map<IList<InventoryOutputModel>>(await inventoryRepository.GetInventories(cancellationToken));
        }
    }
}
