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
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(IMapper mapper, ICategoryRepository categoryRepository)
        {
            this.mapper = mapper;
            this.categoryRepository = categoryRepository;
        }
        public async Task<CategoryOutputModel> AddProductType(CategoryInputModel model, CancellationToken cancellationToken)
        {
            var existProductType = await categoryRepository.Any(e => e.Description.Equals(model.Description), cancellationToken);

            if (existProductType)
            {
                throw new Exception("Já existe o tipo do produto informado!");
            }

            var productType = mapper.Map<Category>(model);

            return mapper.Map<CategoryOutputModel>(await categoryRepository.Add(productType, cancellationToken));
        }
    }
}
