using System.Net;
using Common.Shared.SeedWork;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;

namespace Ordering.API.Controllers.V1
{
    public class OrderController : BaseController
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<PagedList<OrderDto>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ApiResult<PagedList<OrderDto>>>> GetOrdersByUserName([FromQuery] GetOrdersListQuery query)
        {
            var orders = await _mediator.Send(query);

            var result = new ApiResult<PagedList<OrderDto>>
            {
                Message = "Success",
                IsSuccessed = true,
                ResultObj = orders
            };

            return Ok(result);
        }

        // testing purpose
        [HttpPost(Name = "CheckoutOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckoutOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut(Name = "UpdateOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteOrder(Guid? id)
        {
            var command = new DeleteOrderCommand() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}