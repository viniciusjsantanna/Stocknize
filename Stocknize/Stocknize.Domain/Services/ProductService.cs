using AutoMapper;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Exceptions;
using Stocknize.Domain.Interfaces.Domain;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Domain.Models.Products;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }
        public async Task<ProductOutputModel> AddProduct(ProductInputModel productModel, CancellationToken cancellationToken)
        {
            var existProduct = await productRepository.Any(e => e.Name.Equals(productModel.Name), cancellationToken);

            if (existProduct)
            {
                throw new System.Exception("O produto informado já existe!");
            }

            var result = await productRepository.Add(mapper.Map<Product>(productModel), cancellationToken);

            return mapper.Map<ProductOutputModel>(result);
        }

        public async Task DeleteProduct(System.Guid productId, CancellationToken cancellationToken)
        {
            var product = await productRepository.Get(e => e.Id.Equals(productId), cancellationToken)
                ?? throw new NotFoundException("Não foi possível encontrar o produto");

            await productRepository.Delete(product, cancellationToken);
        }

        public async Task<ProductOutputModel> UpdateProduct(System.Guid productId, ProductInputModel productModel, CancellationToken cancellationToken)
        {
            var product = await productRepository.Get(e => e.Id.Equals(productId), cancellationToken)
                            ?? throw new System.Exception("Não foi possível encontrar o produto");

            mapper.Map(productModel, product);

            var result = await productRepository.Update(product, cancellationToken);

            return mapper.Map<ProductOutputModel>(result);
        }
    }
}
