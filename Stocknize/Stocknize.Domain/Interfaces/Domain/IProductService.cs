using Stocknize.Domain.Models.Products;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Domain.Interfaces.Domain
{
    public interface IProductService
    {
        Task<ProductAddOutputModel> AddProduct(ProductInputModel productModel, CancellationToken cancellationToken);
    }
}
