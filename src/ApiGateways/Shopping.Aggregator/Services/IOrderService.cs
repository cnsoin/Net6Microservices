
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public interface IOrderService
    {
        Task<ApiResult<PagedList<OrderResponseModel>>> GetOrdersByUserName(string userName, int pageNumber, int pageSize);
    }
}
