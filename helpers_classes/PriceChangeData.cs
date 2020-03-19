using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDatabase
{
    public class PriceChangeData
    {
        public decimal new_markup { get; set; }
        public decimal new_price { get; set; }
        public int branch_id { get; set; }
        public int product_id { get; set; }
        public string edited_by { get; set; }

    }
}
