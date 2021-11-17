using AutoMapper;
using Catalog.Application.Contracts.Persistence;
using Common.Shared.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Features.Products.Queries.GetProductsList
{
    public class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, PagedList<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProductsListQuery> _logger;

        public GetProductsListQueryHandler(IProductRepository productRepository, IMapper mapper, ILogger<GetProductsListQuery> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PagedList<ProductDto>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"BEGIN: {nameof(GetProductsListQuery)}");
            var query = await _productRepository.GetAllAsync(request.PageNumber, request.PageSize);
            var items = _mapper.Map<List<ProductDto>>(query);
            _logger.LogInformation($"END: {nameof(GetProductsListQuery)}");
            return new PagedList<ProductDto>(items, items.Count, request.PageNumber, request.PageSize);
        }
    }
}