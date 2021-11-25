using Catalog.Application.Features.Products.Queries.GetProductsList;
using MediatR;

namespace Catalog.Application.Features.Products.Queries.GetProduct
{
    public class GetProductQuery : IRequest<ProductDto>
    {
        public Guid? Id { get; set; }
    }
}