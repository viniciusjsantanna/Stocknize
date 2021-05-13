using AutoMapper;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Interfaces.Domain;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Domain.Models.Inventories;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Domain.Services
{
    public class MovimentationService : IMovimentationService
    {
        private readonly IProductRepository productRepository;
        private readonly IUserRepository userRepository;
        private readonly IMovimentationRepository movimentationRepository;
        private readonly IInventoryService inventoryService;
        private readonly IMapper mapper;

        public MovimentationService(IProductRepository productRepository, IUserRepository userRepository, 
            IMovimentationRepository movimentationRepository, IInventoryService inventoryService, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.userRepository = userRepository;
            this.movimentationRepository = movimentationRepository;
            this.inventoryService = inventoryService;
            this.mapper = mapper;
        }
        public async Task<MovimentationOutputModel> AddMovimentation(MovimentationInputModel movimentationModel, CancellationToken cancellationToken)
        {
            var movimentation = mapper.Map<Movimentation>(movimentationModel);
            movimentation.Product = await productRepository.Get(e => e.Id.Equals(movimentationModel.ProductId), cancellationToken);
            movimentation.User = await userRepository.Get(e => e.Id.Equals(movimentationModel.UserId), cancellationToken);

            await inventoryService.UpdateInventory(movimentationModel, cancellationToken);
            
            var result = await movimentationRepository.Add(movimentation, cancellationToken);
            
            return mapper.Map<MovimentationOutputModel>(result);
        }
    }
}
