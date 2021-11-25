using AutoMapper;
using Catalog.Application.Contracts.Persistence;
using Catalog.Application.Exceptions;
using Catalog.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteProductCommandHandler> _logger;

        public DeleteProductCommandHandler(IProductRepository productRepository, IMapper mapper, ILogger<DeleteProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"BEGIN: {nameof(DeleteProductCommand)}");
            var productToDelete = await _productRepository.GetByIdAsync(request.Id);
            if (productToDelete == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            _mapper.Map(request, productToDelete, typeof(DeleteProductCommand), typeof(Product));

            await _productRepository.DeleteAsync(productToDelete);

            _logger.LogInformation($"product {productToDelete.Id} is successfully Deleted.");
            _logger.LogInformation($"END: {nameof(DeleteProductCommand)}");

            return Unit.Value;
        }
    }
}