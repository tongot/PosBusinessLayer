using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDatabase
{
   public class ProductToTax
    {
        public int ProductToTaxId { get; set; }
        public int ProductId { get; set; }
        public int TaxId { get; set; }
        public virtual Tax Tax { get; set; }
        public virtual Product Product { get; set; }
    }
}
