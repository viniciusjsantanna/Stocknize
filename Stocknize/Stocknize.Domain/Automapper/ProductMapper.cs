using AutoMapper;
using Stocknize.Crosscutting.Extensions;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Models.Products;

namespace Stocknize.Domain.Automapper
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductOutputModel>()
                .ForMember(e => e.Type, opt => opt.MapFrom(e => e.Type.Description))
                .ForMember(e => e.CompanyName, opt => opt.MapFrom(e => e.Company.Name));

            CreateMap<ProductInputModel, Product>();
        }
    }
}
