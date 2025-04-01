using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Product
    {
        public Product()
        { }

        public Product(int productId, string productName, string productBrand, string productBarcode, int stock = 0)
        {
            ProductId = productId;
            ProductName = productName;
            ProductBrand = productBrand;
            ProductBarcode = productBarcode;
            Stock = stock;
        }

        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductBrand { get; set; }
        public string ProductBarcode { get; set; }
        public int Stock { get; set; }
    }
}
