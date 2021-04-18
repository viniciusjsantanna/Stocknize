using Stocknize.Domain.Entities;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Infrastructure.Context;

namespace Stocknize.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(EFContext context) : base(context)
        {

        }
    }
}
