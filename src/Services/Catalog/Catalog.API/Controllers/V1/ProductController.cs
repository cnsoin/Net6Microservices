using System.Net;
using Catalog.Application.Features.Products.Commands.CreateProducts;
using Catalog.Application.Features.Products.Commands.DeleteProduct;
using Catalog.Application.Features.Products.Commands.UpdateProducts;
using Catalog.Application.Features.Products.Queries.GetProduct;
using Catalog.Application.Features.Products.Queries.GetProductsList;
using Common.Shared.SeedWork;
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
        [ProducesResponseType(typeof(ApiResult<PagedList<ProductDto>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ApiResult<PagedList<ProductDto>>>> GetAllProductsAsync([FromQuery] GetProductsListQuery query)
        {
            var products = await _mediator.Send(query);
            var result = new ApiResult<PagedList<ProductDto>>
            {
                Message = "Success",
                IsSuccessed = true,
                ResultObj = products
            };

            return Ok(result);
        }

        // testing purpose
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateProductAsync([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<ProductDto>>> GetProductByIdAsync(Guid? id)
        {
            var product = await _mediator.Send(new GetProductQuery
            {
                Id = id
            });

            var result = new ApiResult<ProductDto>
            {
                Message = "Success",
                IsSuccessed = true,
                ResultObj = product
            };

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProductAsync([FromBody] UpdateProductCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductAsync(Guid? id)
        {
            var command = new DeleteProductCommand() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}