namespace ClientApp.Pages.Catalog
{
    public partial class Product
    {
        [Inject]
        private ICatalogApiClient _catalogApiClient { get; set; }
        public ApiResult<PagedList<ProductModel>> ProductModel = new ApiResult<PagedList<ProductModel>>();
        protected override async Task OnInitializedAsync()
        {
            ProductModel = await _catalogApiClient.GetAllProductsAsync(1, 50);
        }
    }
}