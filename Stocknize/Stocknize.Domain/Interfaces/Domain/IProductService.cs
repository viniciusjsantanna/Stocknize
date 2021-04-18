using Stocknize.Domain.Models.Products;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Domain.Interfaces.Domain
{
    public interface IProductService
    {
        Task<ProductOutputModel> AddProduct(ProductInputModel productModel, CancellationToken cancellationToken);
        Task<ProductOutputModel> UpdateProduct(Guid productId, ProductInputModel productModel, CancellationToken cancellationToken);
        Task DeleteProduct(Guid productId, CancellationToken cancellationToken);
    }
}
