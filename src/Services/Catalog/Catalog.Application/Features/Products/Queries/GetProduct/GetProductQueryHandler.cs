using AutoMapper;
using Catalog.Application.Contracts.Persistence;
using Catalog.Application.Features.Products.Queries.GetProductsList;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Features.Products.Queries.GetProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProductsListQuery> _logger;

        public GetProductQueryHandler(IProductRepository productRepository, IMapper mapper, ILogger<GetProductsListQuery> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"BEGIN: {nameof(GetProductQueryHandler)}");
            var query = await _productRepository.GetByIdAsync(request.Id);
            var items = _mapper.Map<ProductDto>(query);
            _logger.LogInformation($"END: {nameof(GetProductQueryHandler)}");
            return items;
        }
    }
}