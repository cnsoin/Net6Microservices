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
            var response = await _httpClient.GetAsync($"/Catalog?pageNumber={pageNumber}&pageSize={pageSize}");
            //var result = await response.ReadContentAs<ApiResult<PagedList<OrderResponseModel>>>();

            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResult<PagedList<ProductModel>>>(jsonResult);

            if (result == null)
            {
                return null;
            }
            return result;
        }
    }
}