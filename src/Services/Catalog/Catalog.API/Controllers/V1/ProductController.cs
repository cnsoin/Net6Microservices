using System.Net;
using Catalog.Application.Features.Products.Commands.CreateProducts;
using Catalog.Application.Features.Products.Queries.GetProductsList;
using MediatR;

namespace Catalog.API.Controllers.V1
{
    public class ProductController : BaseController
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProductsAsync([FromQuery] GetProductsListQuery query)
        {
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }

        // testing purpose
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateProductAsync([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}