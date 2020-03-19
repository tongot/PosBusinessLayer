using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDatabase
{
    public class ProductToDiscount
    {
        public int ProductToDiscountId { get; set; }
        public int DiscountId { get; set; }
        public int ProductId { get; set; }
        public virtual Discount Discount { get; set; }
        public virtual Product Product { get; set; }
    }
}
