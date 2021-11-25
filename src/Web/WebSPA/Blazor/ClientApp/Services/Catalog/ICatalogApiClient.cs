using ClientApp.Model;
using Common.Shared.SeedWork;

namespace ClientApp.Services.Catalog
{
    public interface ICatalogApiClient
    {
        Task<ApiResult<PagedList<ProductModel>>> GetAllProductsAsync(int pageNumber, int pageSize);
    }
}