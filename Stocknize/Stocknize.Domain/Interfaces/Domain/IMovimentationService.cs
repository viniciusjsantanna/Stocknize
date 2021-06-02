using Stocknize.Domain.Models.Inventories;
using System.Threading.Tasks;
using System.Threading;

namespace Stocknize.Domain.Interfaces.Domain
{
    public interface IMovimentationService
    {
        Task<MovimentationOutputModel> AddMovimentation(MovimentationInputModel movimentationModel, CancellationToken cancellationToken);
    }
}
