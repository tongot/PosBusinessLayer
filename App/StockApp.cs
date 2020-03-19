using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDatabase
{
    public class StockApp : IStock
    {
        ApplicationContext _context = new ApplicationContext();
        private IStockTrack _stockTrack;
        public void addNewStock(Stock stock)
        {
            _context.stocks.Add(stock);
            _context.SaveChanges();
            _stockTrack = new StockTrackApp();
        }
        public Stock getStockById(Guid id)
        {
            return _context.stocks.Find(id);
        }
        public IEnumerable<Stock> getStockForBranchProduct(int product_id, int branch_id)
        {
            var stocks = _context.stocks.Where(x => x.ProductId == product_id & branch_id == x.BranchId);
            return stocks;
        }

        public IEnumerable<Stock> getStocksForBranch(int branch_id)
        {
            var stocks = _context.stocks.Where(x=> branch_id == x.BranchId);
            return stocks;
        }

        public IEnumerable<Stock> getStocksForProduct(int product_id)
        {
            var stocks = _context.stocks.Where(x => x.ProductId == product_id );
            return stocks;
        }

        public void updatePrice(PriceChangeData pice_change, bool for_all)
        {
           
        }

        public Stock getRunningStock(int product_id, int branch_id)
        {
            Stock stock = new Stock();
            //get current running stock
            var current_stock = _context.stocks.Where(x => x.ProductId == product_id & x.BranchId == branch_id & x.is_running_stock).FirstOrDefault();
            if(current_stock==null)
            {
                current_stock =_context.stocks.Where(x => x.ProductId == product_id & x.BranchId == branch_id ).OrderBy(x => x.date_of_stock).FirstOrDefault();
                if (current_stock == null)
                {
                    return null;
                }
                current_stock.is_running_stock = true;
                updateStock(current_stock);

            }

            if (current_stock.current_running_stock > 0)
            {
                stock = current_stock;
                return stock;
            }

            //check if the current runing stock has items if zero get next stock
            if(current_stock.current_running_stock==0)
            {
                //get the next stock
                var next_batch= _context.stocks.Where(x => x.ProductId == product_id & x.BranchId == branch_id & x.date_of_stock>current_stock.date_of_stock).OrderBy(x=>x.date_of_stock).FirstOrDefault();
                if(next_batch!=null)
                {
                    Stock st = next_batch;
                    st.is_running_stock = true;

                    if (updateStock(st))
                    {
                        stock = next_batch;
                        current_stock.is_running_stock = false;
                        updateStock(current_stock);
                    }
                     
                }
                return stock;
            }
            return stock;
                
        }

        bool updateStock(Stock stock)
        {
            try
            {
                _context.Set<Stock>().AddOrUpdate(stock);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
