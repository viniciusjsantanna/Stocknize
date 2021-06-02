using AutoMapper;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Models.Category;

namespace Stocknize.Domain.Automapper
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<CategoryInputModel, Category>();
            CreateMap<Category, CategoryOutputModel>();
        }
    }
}
