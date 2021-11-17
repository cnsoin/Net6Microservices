namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQuery : PagingParameters, IRequest<PagedList<OrderDto>>
    {
        public string UserName { get; set; }
    }
}