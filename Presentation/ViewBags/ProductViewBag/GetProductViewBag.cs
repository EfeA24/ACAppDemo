namespace Presentation.ViewBags.ProductViewBag
{
    public class GetProductViewBag
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductBrand { get; set; }
        public string ProductBarcode { get; set; }
        public int? Stock { get; set; }
    }
}
