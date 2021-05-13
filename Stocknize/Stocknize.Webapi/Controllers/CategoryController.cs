using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stocknize.Domain.Interfaces.Domain;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Domain.Models.Category;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Webapi.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService productTypeService;
        private readonly IMapper mapper;

        public CategoryController(ICategoryService productTypeService, IMapper mapper)
        {
            this.productTypeService = productTypeService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<CategoryOutputModel> Post(CategoryInputModel model, CancellationToken cancellationToken)
        {
            return await productTypeService.AddProductType(model, cancellationToken);
        }

        [HttpGet]
        public async Task<IList<CategoryOutputModel>> Get([FromServices] ICategoryRepository productTypeRepository,CancellationToken cancellationToken)
        {
            return mapper.Map<IList<CategoryOutputModel>>(await productTypeRepository.GetProductTypes(cancellationToken));
        }
    }
}
