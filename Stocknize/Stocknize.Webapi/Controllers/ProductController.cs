using Microsoft.AspNetCore.Mvc;
using Stocknize.Domain.Interfaces.Domain;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Domain.Models.Products;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Stocknize.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IProductService productService;

        public ProductController(IProductRepository productRepository, IProductService productService)
        {
            this.productRepository = productRepository;
            this.productService = productService;
        }

        [HttpPost]
        public Task<ProductAddOutputModel> Post([FromBody] ProductInputModel productModel, CancellationToken cancellationToken)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var result = productService.AddProduct(productModel, cancellationToken);
            transaction.Complete();

            return result;
        }

    }
}
