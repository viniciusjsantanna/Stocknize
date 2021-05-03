using AutoMapper;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Interfaces.Domain;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Domain.Models.ProductType;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Domain.Services
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IMapper mapper;
        private readonly IProductTypeRepository productTypeRepository;

        public ProductTypeService(IMapper mapper, IProductTypeRepository productTypeRepository)
        {
            this.mapper = mapper;
            this.productTypeRepository = productTypeRepository;
        }
        public async Task<ProductTypeOutputModel> AddProductType(ProductTypeInputModel model, CancellationToken cancellationToken)
        {
            var existProductType = await productTypeRepository.Any(e => e.Description.Equals(model.Description), cancellationToken);

            if (existProductType)
            {
                throw new Exception("Já existe o tipo do produto informado!");
            }

            var productType = mapper.Map<ProductType>(model);

            return mapper.Map<ProductTypeOutputModel>(await productTypeRepository.Add(productType, cancellationToken));
        }
    }
}
