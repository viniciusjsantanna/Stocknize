using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stocknize.Crosscutting.Extensions;
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
            return mapper.Map<IList<InventoryOutputModel>>(await inventoryRepository.GetInventories(HttpContext.GetLoggedUserCompany(), cancellationToken));
        }


    }
}
