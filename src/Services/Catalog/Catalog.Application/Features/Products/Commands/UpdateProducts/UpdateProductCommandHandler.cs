using AutoMapper;
using Catalog.Application.Contracts.Persistence;
using Catalog.Application.Exceptions;
using Catalog.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Features.Products.Commands.UpdateProducts
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper, ILogger<UpdateProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"BEGIN: {nameof(UpdateProductCommand)}");
            var productToUpdate = await _productRepository.GetByIdAsync(request.Id);
            if (productToUpdate == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            _mapper.Map(request, productToUpdate, typeof(UpdateProductCommand), typeof(Product));

            await _productRepository.UpdateAsync(productToUpdate);

            _logger.LogInformation($"product {productToUpdate.Id} is successfully updated.");
            _logger.LogInformation($"END: {nameof(UpdateProductCommand)}");

            return Unit.Value;
        }
    }
}