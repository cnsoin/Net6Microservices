using MediatR;

namespace Catalog.Application.Features.Products.Commands.UpdateProducts
{
    public class UpdateProductCommand : IRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
}