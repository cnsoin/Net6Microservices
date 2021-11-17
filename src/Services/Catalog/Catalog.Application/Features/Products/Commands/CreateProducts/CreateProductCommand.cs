using MediatR;

namespace Catalog.Application.Features.Products.Commands.CreateProducts
{
    public class CreateProductCommand : IRequest<Guid?>
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string? ImageFile { get; set; }
        public decimal Price { get; set; }
    }
}