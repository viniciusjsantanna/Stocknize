using Stocknize.Domain.Entities;
using Stocknize.Domain.Exceptions;
using Stocknize.Domain.Interfaces.Domain;
using Stocknize.Domain.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Domain.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IProductRepository productRepository;
        private readonly IInventoryRepository inventoryRepository;

        public InventoryService(IProductRepository productRepository, IInventoryRepository inventoryRepository)
        {
            this.productRepository = productRepository;
            this.inventoryRepository = inventoryRepository;
        }

        public async Task AddInventory(Guid productId, int quantity, CancellationToken cancellationToken)
        {
            var product = await productRepository.Get(e => e.Id.Equals(productId), cancellationToken)
                ?? throw new NotFoundException("Não foi possível adicionar o produto!");

            var inventory = new Inventory(product, quantity);

            await inventoryRepository.Add(inventory, cancellationToken);
        }
    }
}
