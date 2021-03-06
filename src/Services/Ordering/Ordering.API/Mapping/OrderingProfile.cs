using AutoMapper;
using EventBus.Messages.Event;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace Ordering.API.Controllers.Mapping
{
    public class OrderingProfile : Profile
    {
        public OrderingProfile()
        {
            CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>().ReverseMap();
        }
    }
}