using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stocknize.Domain.Interfaces.Domain;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Domain.Models.ProductType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Webapi.Controllers
{
    [Route("api/productTypes")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeService productTypeService;
        private readonly IMapper mapper;

        public ProductTypeController(IProductTypeService productTypeService, IMapper mapper)
        {
            this.productTypeService = productTypeService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ProductTypeOutputModel> Post(ProductTypeInputModel model, CancellationToken cancellationToken)
        {
            return await productTypeService.AddProductType(model, cancellationToken);
        }

        [HttpGet]
        public async Task<IList<ProductTypeOutputModel>> Get([FromServices] IProductTypeRepository productTypeRepository,CancellationToken cancellationToken)
        {
            return mapper.Map<IList<ProductTypeOutputModel>>(await productTypeRepository.GetProductTypes(cancellationToken));
        }
    }
}
