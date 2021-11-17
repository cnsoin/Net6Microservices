using Common.Shared.SeedWork;
using MediatR;

namespace Catalog.Application.Features.Products.Queries.GetProductsList
{
    public class GetProductsListQuery : PagingParameters, IRequest<PagedList<ProductDto>>
    {

    }
}