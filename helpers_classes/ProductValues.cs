using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDatabase
{
    public class ProductValues
    {
        public int quantity_of_products { get; set; }

        public decimal product_value { get; set; }
        public int number_of_Product { get; set; }
        public Product high_stock_product { get; set; }
        public Product low_stock_product { get; set; }
        public List<Product> out_of_stock_product { get; set; }
    }
}
