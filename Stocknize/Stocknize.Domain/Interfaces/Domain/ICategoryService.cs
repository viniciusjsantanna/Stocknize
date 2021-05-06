using Stocknize.Domain.Models.Category;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Domain.Interfaces.Domain
{
    public interface ICategoryService
    {
        Task<CategoryOutputModel> AddProductType(CategoryInputModel model, CancellationToken cancellationToken);
    }
}
