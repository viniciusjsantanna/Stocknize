using AutoMapper;
using Stocknize.Crosscutting.Extensions;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Models.Inventories;

namespace Stocknize.Domain.Automapper
{
    class InventoryMapper : Profile
    {
        public InventoryMapper()
        {
            CreateMap<Inventory, InventoryOutputModel>()
                .ForMember(e => e.Id, opt => opt.MapFrom(e => e.Id))
                .ForMember(e => e.Quantity, opt => opt.MapFrom(e => e.Quantity))
                .ForPath(e => e.ProductId, opt => opt.MapFrom(e => e.Product.Id))
                .ForPath(e => e.ProductName, opt => opt.MapFrom(e => e.Product.Name))
                .ForPath(e => e.ProductType, opt => opt.MapFrom(e => e.Product.Type.GetEnumDescription()));

        }
    }
}
