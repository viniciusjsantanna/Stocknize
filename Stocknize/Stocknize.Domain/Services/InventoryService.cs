using AutoMapper;
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
        private readonly IMapper mapper;
        private readonly IMovimentationRepository movimentationRepository;
        private readonly IUserRepository userRepository;

        public InventoryService(IProductRepository productRepository,
            IInventoryRepository inventoryRepository,
            IMapper mapper,
            IMovimentationRepository movimentationRepository,
            IUserRepository userRepository)
        {
            this.productRepository = productRepository;
            this.inventoryRepository = inventoryRepository;
            this.mapper = mapper;
            this.movimentationRepository = movimentationRepository;
            this.userRepository = userRepository;
        }

        public async Task AddInventory(Guid productId, int quantity, CancellationToken cancellationToken)
        {
            var product = await productRepository.Get(e => e.Id.Equals(productId), cancellationToken)
                ?? throw new NotFoundException("Não foi possível adicionar o produto!");

            var inventory = new Inventory(product, quantity);

            await inventoryRepository.Add(inventory, cancellationToken);
        }

        public async Task<MovimentationOutputModel> AddMovimentation(MovimentationInputModel movimentationModel, CancellationToken cancellationToken)
        {
            var movimentation = mapper.Map<Movimentation>(movimentationModel);
            movimentation.Product = await productRepository.Get(e => e.Id.Equals(movimentationModel.ProductId), cancellationToken);
            movimentation.User = await userRepository.Get(e => e.Id.Equals(movimentationModel.UserId), cancellationToken);

            var result = await movimentationRepository.Add(movimentation, cancellationToken);

            await UpdateInventory(movimentation, cancellationToken);
            
            return mapper.Map<MovimentationOutputModel>(result);
        }

        private async Task UpdateInventory(Movimentation movimentation, CancellationToken cancellationToken)
        {
            var quantity = 0;
            var inventory = await inventoryRepository.Get(e => e.Product.Id.Equals(movimentation.Product.Id), cancellationToken)
                 ?? throw new NotFoundException("Não foi possível encontrar o estoque do produto informado!");

            if (movimentation.Type.Equals(MovimentationType.Buy))
                quantity = inventory.Quantity + movimentation.Quantity;
            else
                quantity = inventory.Quantity - movimentation.Quantity;

            inventory.ChangeQuantity(quantity);

            await inventoryRepository.Update(inventory, cancellationToken);
        }
    }
}
