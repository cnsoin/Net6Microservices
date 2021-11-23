namespace Shopping.Aggregator.Models
{
    public class ShoppingModel
    {
        public string UserName { get; set; }
        public BasketModel BasketWithProducts { get; set; }
        public ApiResult<PagedList<OrderResponseModel>> Orders { get; set; }
    }
}
