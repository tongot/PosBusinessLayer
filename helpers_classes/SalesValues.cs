
using System;

namespace AppDatabase
{
    public class SalesValues
    {
        public decimal sales_revenue { get; set; }
        public int product_count { get; set; }
        public int sold_units { get; set; }
        public decimal void_sales { get; set; }
        public decimal failed_sales { get; set; }
    }
}
