using AutoMapper;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Interfaces.Domain;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Domain.Models.Products;
using System.Linq;
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
            var existProduct = productRepository.Get(e => e.Name.Equals(productModel.Name)).Any();

            if (existProduct)
            {
                throw new System.Exception("O produto informado já existe!");
            }

            var result = await productRepository.Add(mapper.Map<Product>(productModel), cancellationToken);

            return mapper.Map<ProductOutputModel>(result);
        }
    }
}
