using AutoMapper;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Models.ProductType;

namespace Stocknize.Domain.Automapper
{
    public class ProductTypeMapper : Profile
    {
        public ProductTypeMapper()
        {
            CreateMap<ProductTypeInputModel, ProductType>();
            CreateMap<ProductType, ProductTypeOutputModel>();
        }
    }
}
