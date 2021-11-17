namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder;
public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, Guid?>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly ILogger<CheckoutOrderCommandHandler> _logger;

    public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService, ILogger<CheckoutOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<Guid?> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"BEGIN: {nameof(CheckoutOrderCommandHandler)}");
        var req = _mapper.Map<Order>(request);
        var newOrder = await _orderRepository.AddAsync(req);
        _logger.LogInformation($"Order {newOrder.Id} is successfully created.");

        await SendMail(newOrder);

        _logger.LogInformation($"END: {nameof(CheckoutOrderCommandHandler)}");
        return newOrder.Id;
    }

    private async Task SendMail(Order order)
    {
        var email = new Email()
        {
            To = "taolamaj789456@gmail.com",
            Body = $"Order was created.",
            Subject = "Order was created"
        };
        try
        {
            await _emailService.SendEmail(email);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Order {order.Id} failed due to an error with the mail service: {ex.Message}");
        }
    }
}