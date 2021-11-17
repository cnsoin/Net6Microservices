namespace Catalog.Application.Features.Products.Queries.GetProductsList
{
    public class ProductDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string? ImageFile { get; set; }
        public decimal Price { get; set; }
    }
}