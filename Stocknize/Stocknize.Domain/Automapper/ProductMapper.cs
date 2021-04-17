using AutoMapper;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Models.Products;

namespace Stocknize.Domain.Automapper
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductAddOutputModel>();

            CreateMap<ProductInputModel, Product>();
        }
    }
}
