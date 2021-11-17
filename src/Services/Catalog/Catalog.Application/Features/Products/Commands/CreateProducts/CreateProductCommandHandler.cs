using AutoMapper;
using Catalog.Application.Contracts.Persistence;
using Catalog.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Features.Products.Commands.CreateProducts
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid?>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductCommandHandler> _logger;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper, ILogger<CreateProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid?> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"BEGIN: {nameof(CreateProductCommandHandler)}");
            var req = _mapper.Map<Product>(request);
            var query = await _productRepository.AddAsync(req);
            _logger.LogInformation($"Product {query.Id} is successfully created.");

            _logger.LogInformation($"END: {nameof(CreateProductCommandHandler)}");
            return query.Id;
        }
    }
}