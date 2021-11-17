using AutoMapper;
using Catalog.Application.Features.Products.Commands.CreateProducts;
using Catalog.Application.Features.Products.Queries.GetProductsList;
using Catalog.Domain.Entities;

namespace Catalog.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, CreateProductCommand>().ReverseMap();
        }
    }
}