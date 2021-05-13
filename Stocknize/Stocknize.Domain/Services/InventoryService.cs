using Stocknize.Domain.Entities;
using Stocknize.Domain.Enums;
using Stocknize.Domain.Exceptions;
using Stocknize.Domain.Interfaces.Domain;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Domain.Models.Inventories;
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

        public async Task UpdateInventory(MovimentationInputModel movimentationModel, CancellationToken cancellationToken)
        {
            var quantity = 0;
            var inventory = await inventoryRepository.Get(e => e.Product.Id.Equals(movimentationModel.ProductId), cancellationToken)
                 ?? throw new NotFoundException("Não foi possível encontrar o estoque do produto informado!");

            if (movimentationModel.Type.Equals(MovimentationType.Buy))
                quantity = inventory.Quantity + movimentationModel.Quantity;
            else
                quantity = inventory.Quantity - movimentationModel.Quantity;

            inventory.ChangeQuantity(quantity);

            await inventoryRepository.Update(inventory, cancellationToken);
        }
    }
}
