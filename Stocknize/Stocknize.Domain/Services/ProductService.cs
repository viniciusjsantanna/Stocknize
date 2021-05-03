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
        private readonly IInventoryService inventoryService;
        private readonly ICompanyRepository companyRepository;
        private readonly IMapper mapper;
        private readonly IProductTypeRepository productTypeRepository;
        private const int DEFAULT_INVENTORY_QUANTITY = 0;

        public ProductService(IProductRepository productRepository, IInventoryService inventoryService,
            ICompanyRepository companyRepository, IMapper mapper, IProductTypeRepository productTypeRepository)
        {
            this.productRepository = productRepository;
            this.inventoryService = inventoryService;
            this.companyRepository = companyRepository;
            this.mapper = mapper;
            this.productTypeRepository = productTypeRepository;
        }
        public async Task<ProductOutputModel> AddProduct(ProductInputModel productModel, CancellationToken cancellationToken)
        {
            var existProduct = await productRepository.Any(e => e.Name.Equals(productModel.Name), cancellationToken);

            if (existProduct)
            {
                throw new System.Exception("O produto informado já existe!");
            }

            var product = mapper.Map<Product>(productModel);
            product.Company = await companyRepository.Get(e => e.Id.Equals(productModel.CompanyId), cancellationToken);
            product.Type = await productTypeRepository.Get(e => e.Id.Equals(productModel.ProductTypeId), cancellationToken);

            var result = await productRepository.Add(product, cancellationToken);

            await inventoryService.AddInventory(result.Id, DEFAULT_INVENTORY_QUANTITY, cancellationToken);

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
                            ?? throw new NotFoundException("Não foi possível encontrar o produto");

            mapper.Map(productModel, product);

            var result = await productRepository.Update(product, cancellationToken);

            return mapper.Map<ProductOutputModel>(result);
        }
    }
}
