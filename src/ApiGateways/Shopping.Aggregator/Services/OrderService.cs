
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _client;

        public OrderService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<ApiResult<PagedList<OrderResponseModel>>> GetOrdersByUserName(string userName, int pageNumber, int pageSize)
        {
            var response = await _client.GetAsync($"/api/v1/Order?UserName={userName}&PageNumber={pageNumber}&PageSize={pageSize}");
            //var result = await response.ReadContentAs<ApiResult<PagedList<OrderResponseModel>>>();

            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResult<PagedList<OrderResponseModel>>>(jsonResult);

            return result;

        }
    }
}
