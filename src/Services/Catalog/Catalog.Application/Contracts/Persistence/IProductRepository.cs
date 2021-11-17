using Catalog.Domain.Entities;

namespace Catalog.Application.Contracts.Persistence
{
    public interface IProductRepository : IAsyncRepository<Product>
    {

    }
}