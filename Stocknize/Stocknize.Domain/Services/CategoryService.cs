using AutoMapper;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Interfaces.Domain;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Domain.Models.Category;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper mapper;
        private readonly ICategoryRepository productTypeRepository;

        public CategoryService(IMapper mapper, ICategoryRepository productTypeRepository)
        {
            this.mapper = mapper;
            this.productTypeRepository = productTypeRepository;
        }
        public async Task<CategoryOutputModel> AddProductType(CategoryInputModel model, CancellationToken cancellationToken)
        {
            var existProductType = await productTypeRepository.Any(e => e.Description.Equals(model.Description), cancellationToken);

            if (existProductType)
            {
                throw new Exception("Já existe o tipo do produto informado!");
            }

            var productType = mapper.Map<Category>(model);

            return mapper.Map<CategoryOutputModel>(await productTypeRepository.Add(productType, cancellationToken));
        }
    }
}
