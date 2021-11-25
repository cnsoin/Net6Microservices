
namespace ClientApp.Services.Catalog
{
    public class CatalogApiClient : ICatalogApiClient
    {
        private readonly HttpClient _httpClient;

        public CatalogApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResult<PagedList<ProductModel>>> GetAllProductsAsync(int pageNumber, int pageSize)
        {
            //var response = await _httpClient.GetAsync($"/Catalog?pageNumber={pageNumber}&pageSize={pageSize}");
            //var result = await response.ReadContentAs<ApiResult<PagedList<OrderResponseModel>>>();
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = pageNumber.ToString(),
                ["pageSize"] = pageSize.ToString()
            };
            string url = QueryHelpers.AddQueryString("Catalog", queryStringParam);
            var response = await _httpClient.GetAsync(url);

            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResult<PagedList<ProductModel>>>(jsonResult);

            return result;
        }

        public async Task<ApiResult<ProductModel>> GetProductByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"/Catalog/{id}");

            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResult<ProductModel>>(jsonResult);

            return result;
        }
    }
}