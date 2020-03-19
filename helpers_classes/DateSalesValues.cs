using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDatabase
{
    public class DateSalesValues
    {
        #region for sales revenue chart
        public List<decimal> sales_revenue { get; set; }
        public List<string> date { get; set; }
        #endregion

        #region for total revenue and number of products sold
        public decimal total_sales_revenue { get; set; } = 0;
        public int number_of_products { get; set; } = 0;
        #endregion

        #region for products sold chart
        public List<string> product_name { get; set; }
        public List<int> products_sold { get; set; }
        #endregion

    }
}
