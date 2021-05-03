using Stocknize.Domain.Models.ProductType;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Domain.Interfaces.Domain
{
    public interface IProductTypeService
    {
        Task<ProductTypeOutputModel> AddProductType(ProductTypeInputModel model, CancellationToken cancellationToken);
    }
}
