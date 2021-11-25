
namespace ClientApp.Pages.Catalog
{
    public partial class Product
    {
        [Inject]
        private ICatalogApiClient _catalogApiClient { get; set; }

        [Parameter]
        public int PageNumber { get; set; } = 1;
        [Parameter]
        public int PageSize { get; set; } = 10;

        public ApiResult<PagedList<ProductModel>> ProductModel { get; set; }
        public List<ProductModel> Products { get; set; }
        public MetaData MetaData { get; set; } = new MetaData();

        protected override async Task OnInitializedAsync()
        {
            await GetProducts();
        }
        private async Task GetProducts()
        {
            var pagination = await _catalogApiClient.GetAllProductsAsync(PageNumber, PageSize);
            Products = pagination.ResultObj.Items;
            MetaData = pagination.ResultObj.MetaData;
        }

        private async Task SelectedPage(int pageNumber)
        {
            PageNumber = pageNumber;
            await GetProducts();
        }
    }
}