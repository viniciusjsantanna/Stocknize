using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stocknize.Domain.Interfaces.Domain;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Stocknize.Webapi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ProductController(IProductRepository productRepository, IProductService productService, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.productService = productService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ProductOutputModel> Post([FromBody] ProductInputModel productModel, CancellationToken cancellationToken)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var result = await productService.AddProduct(productModel, cancellationToken);
            transaction.Complete();

            return result;
        }

        [HttpGet]
        public async Task<IList<ProductOutputModel>> GetProducts(CancellationToken cancellationToken)
        {
            return mapper.Map<IList<ProductOutputModel>>(await productRepository.GetAll(cancellationToken));
        }

        [HttpGet]
        [Route("getById")]
        public async Task<ProductOutputModel> GetProductById([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            return mapper.Map<ProductOutputModel>(await productRepository.Get(e => e.Id.Equals(id), cancellationToken));
        }

        [HttpPut]
        public async Task<ProductOutputModel> Put([FromQuery] Guid id, [FromBody] ProductInputModel productModel, CancellationToken cancellationToken)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var result = await productService.UpdateProduct(id, productModel, cancellationToken);
            transaction.Complete();

            return result;
        }

        [HttpDelete]
        public async Task Delete([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            await productService.DeleteProduct(id, cancellationToken);
            transaction.Complete();
        }

    }
}
