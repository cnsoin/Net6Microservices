namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;
public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, PagedList<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetOrdersListQueryHandler> _logger;

    public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<GetOrdersListQueryHandler> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PagedList<OrderDto>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"BEGIN: {nameof(GetOrdersListQueryHandler)}");
        var orderList = await _orderRepository.GetOrdersByUserName(request.PageNumber, request.PageSize, request.UserName);
        var items = _mapper.Map<List<OrderDto>>(orderList);
        _logger.LogInformation($"END: {nameof(GetOrdersListQueryHandler)}");
        return new PagedList<OrderDto>(items, items.Count, request.PageNumber, request.PageSize);
    }
}