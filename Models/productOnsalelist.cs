using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDatabase
{
    public class productOnsalelist
    {
        public int productOnsalelistId{ get; set; }
        public  int ProductId { get; set; }
        public  int BranchId { get; set; }
        public int quantity { get; set; }
        public string   username { get; set; }
        public DateTime date { get; set; }
    }
}
