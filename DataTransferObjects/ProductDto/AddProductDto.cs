using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.ProductDto
{
    public class AddProductDto
    {
        public string ProductName { get; set; }
        public string ProductBrand { get; set; }
        public string ProductBarcode { get; set; }
        public int? Stock { get; set; }

    }
}
