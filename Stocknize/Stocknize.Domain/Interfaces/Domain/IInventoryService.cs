using System;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Domain.Interfaces.Domain
{
    public interface IInventoryService
    {
        Task AddInventory(Guid productId, int quantity, CancellationToken cancellationToken);
    }
}
