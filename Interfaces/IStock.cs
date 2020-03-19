using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDatabase
{
   public  interface IStock
    {
        void addNewStock(Stock stock);
        Stock getStockById(Guid id);
        IEnumerable<Stock> getStocksForProduct(int product_id);
        IEnumerable<Stock> getStocksForBranch(int branch_id);
        IEnumerable<Stock> getStockForBranchProduct(int product_id, int branch_id);
        void updatePrice(PriceChangeData pice_change, bool for_all);

        Stock getRunningStock(int product_id, int branch_id);
    }
}
 