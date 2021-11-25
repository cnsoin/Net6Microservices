namespace ClientApp.Pages.Catalog
{
    public partial class ProductDetail
    {
        [Inject]
        private ICatalogApiClient _catalogApiClient { get; set; }
        [Parameter]
        public string Id { get; set; }
        public ApiResult<ProductModel> Product { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Product = await _catalogApiClient.GetProductByIdAsync(Id);
        }
    }
}