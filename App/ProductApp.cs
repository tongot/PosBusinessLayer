using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

namespace AppDatabase
{
    public class ProductApp : IProduct
    {
        private ApplicationContext _context;
        private IStockTrack _stockTrack;
        private ITax _tax;
        private IDiscount _discount;
        private IStock _stock;


        public ProductApp()
        {
            _context = new ApplicationContext();
            _stockTrack = new StockTrackApp();
            _tax = new TaxApp();
            _discount = new AppDiscount();
            _stock = new StockApp();
        }

        public async Task<Product> addProduct(Product product, string username)
        {
            _context.products.Add(product);
            _context.SaveChanges();
            return product;
        }
        public void deleteProduct(int? id)
        {
            if (id != null)
            {
                Product pr = _context.products.Find(id.Value);
                if (pr != null)
                {
                    _context.products.Remove(pr);
                    _context.SaveChanges();
                }

            }
        }
        public List<Product> GetAllProducts(int PageNumber, int ItemsPerPage, ProductSearchFilter filters)
        {
            setZero(filters);
            int skip = (PageNumber - 1) * ItemsPerPage;
            if (!string.IsNullOrWhiteSpace(filters.searchString))
            {

                var result = _context.products.Where(x => x.ProductName.Contains(filters.searchString)

                    & x.Quantity >= filters.minQuantity & x.Quantity <= filters.maxQuantity
                    & x.TotalValue >= filters.minTotalValue & x.TotalValue <= filters.maxTotalValue)
                       .OrderBy(x => x.ProductName).Skip(skip).Take(ItemsPerPage).ToList();
                return result;
            }

            return _context.products.Where(x =>

                   x.Quantity >= filters.minQuantity & x.Quantity <= filters.maxQuantity
                  & x.TotalValue >= filters.minTotalValue & x.TotalValue <= filters.maxTotalValue)
                      .OrderBy(x => x.ProductName).Skip(skip).Take(ItemsPerPage).ToList();

        }
        public Product getProductById(int id)
        {
            var product = _context.products.Find(id);
            return product;
        }
        public List<Sale> getSalesByProduct(int ProductId)
        {
            return _context.sales.Where(x => x.ProductId == ProductId).ToList();
        }
        public List<Product> CodeLikeProducts(string Codestring)
        {
            return _context.products.Where(x => x.ProductCode.Contains(Codestring)).ToList();
        }
        public decimal TotalValue(ProductSearchFilter filters)
        {
            setZero(filters);
            if (!string.IsNullOrWhiteSpace(filters.searchString))
            {
                var result = _context.products.Where(x => x.ProductName.Contains(filters.searchString)
                                    & x.Quantity >= filters.minQuantity & x.Quantity <= filters.maxQuantity
                                    & x.TotalValue >= filters.minTotalValue & x.TotalValue <= filters.maxTotalValue);
                if (result.Count() > 0)
                {
                    return result.Sum(x => x.TotalValue);
                }
                else
                {
                    return 0;
                }
            }

            var result1 = _context.products.Where(x => 
                     x.Quantity >= filters.minQuantity & x.Quantity <= filters.maxQuantity
                    & x.TotalValue >= filters.minTotalValue & x.TotalValue <= filters.maxTotalValue);
            if (result1.Count() > 0)
            {
                return result1.Sum(x => x.TotalValue);
            }
            else
            {
                return 0;
            }
        }
        public int TotalQuantity(ProductSearchFilter filters)
        {
            setZero(filters);
            if (!string.IsNullOrWhiteSpace(filters.searchString))
            {
                var result1 = _context.products.Where(x => x.ProductName.Contains(filters.searchString)
                                        & x.Quantity >= filters.minQuantity & x.Quantity <= filters.maxQuantity
                                        & x.TotalValue >= filters.minTotalValue & x.TotalValue <= filters.maxTotalValue);
                if (result1.Count() > 0)
                {
                    return result1.Sum(x => x.Quantity);
                }
                else
                {
                    return 0;
                }
            }

            var result = _context.products.Where(x =>  x.Quantity >= filters.minQuantity & x.Quantity <= filters.maxQuantity
                  & x.TotalValue >= filters.minTotalValue & x.TotalValue <= filters.maxTotalValue);

            if (result.Count() > 0)
            {
                return result.Sum(x => x.Quantity);
            }
            else
            {
                return 0;
            }



        }
        public int totalProducts(ProductSearchFilter filters)
        {
            setZero(filters);
            if (!string.IsNullOrWhiteSpace(filters.searchString))
                return _context.products.Where(x => x.ProductName.Contains(filters.searchString)
                  & x.Quantity >= filters.minQuantity & x.Quantity <= filters.maxQuantity
                  & x.TotalValue >= filters.minTotalValue & x.TotalValue <= filters.maxTotalValue)
                      .Count();
            return _context.products.Where(x =>
                 x.Quantity >= filters.minQuantity & x.Quantity <= filters.maxQuantity
                  & x.TotalValue >= filters.minTotalValue & x.TotalValue <= filters.maxTotalValue)
                      .Count();
        }
        public async Task updateProduct(Product product)
        {
            _context.Set<Product>().AddOrUpdate(product);
            ////_context.Entry(product).State = EntityState.Modified;
            //if (HasStockChanged(product, oldProduct))
            //{
            //    StockTrackerUpdater(product, username, oldProduct);
            //}
            await _context.SaveChangesAsync();
        }
        public List<Product> GetFilteredProduct(ProductSearchFilter filters, int PageNumber, int ItemsPerPage)
        {

            int skip = (PageNumber - 1) * ItemsPerPage;

            if (!string.IsNullOrWhiteSpace(filters.searchString))
            {
                if (filters.maxPrice > 0 & filters.maxQuantity > 0 & filters.maxTotalValue > 0)
                {
                    return _context.products.Where(x => x.ProductName.Contains(filters.searchString)
                    & x.Quantity >= filters.minQuantity & x.Quantity <= filters.maxQuantity
                    & x.TotalValue >= filters.minTotalValue & x.TotalValue <= filters.maxTotalValue)
                        .OrderBy(x => x.ProductName).Skip(skip).Take(ItemsPerPage).ToList();
                }

                return null;
            }
            return _context.products.Where(x =>
                    x.Quantity >= filters.minQuantity & x.Quantity <= filters.maxQuantity
                   & x.TotalValue >= filters.minTotalValue & x.TotalValue <= filters.maxTotalValue)
                       .OrderBy(x => x.ProductName).Skip(skip).Take(ItemsPerPage).ToList();
        }
        public async Task updateQuantity( int product_id,int Qnt,decimal value, bool isIncrease)
        {
            Product p =  _context.products.Where(x=>x.ProductId== product_id).FirstOrDefault();
            if (!isIncrease)
            {
                p.Quantity -= Qnt;
                p.TotalValue -= value;
            }
            if (isIncrease)
            {
                p.Quantity += Qnt;
                p.TotalValue += value;
            }
            _context.Set<Product>().AddOrUpdate(p);
             _context.SaveChanges();
        }
        public void updateSaleGoods(int product_id, Guid stock_id,bool isIncrease)
        {
            Product p = _context.products.Find(product_id);
            Stock s = _context.stocks.Find(stock_id);
            if (p != null & s != null)
            {

                if (!isIncrease)
                {
                    p.Quantity -= 1;
                    p.TotalValue -= s.price;
                    s.current_revenue += s.price;
                    s.current_running_stock -= 1;
                }
                if (isIncrease)
                {
                    p.Quantity += 1;
                    p.TotalValue += s.price;
                    s.current_revenue -= s.price;
                    s.current_running_stock += 1;
                }
                _context.Set<Product>().AddOrUpdate(p);
                _context.SaveChanges();
            }
        }
        public int getQuantity(int id)
        {
            int qnt = _context.products.Where(x => x.ProductId == id).Select(x => x.Quantity).FirstOrDefault();
            return qnt;
        }
        public Product GetProductByCode(string code,int branch_id)
        {
            ApplicationContext db = new ApplicationContext();
            Product product = db.products.Where(x => x.ProductCode == code).FirstOrDefault();
            

            if (product != null)
            {
                product.running_stock = new Stock();
                product.running_stock = _stock.getRunningStock(product.ProductId, branch_id);

                if(product.running_stock != null)
                {
                        decimal tax = _tax.calculateTaxForProduct(product.ProductId, product.running_stock.price);
                        decimal discount = _discount.calculateDiscountForProduct(product.ProductId, product.running_stock.price);
                    //set product with tax value
                        product.running_stock.price += tax;
                        product.taxToPay = tax;

                    //set Product with discounts
                        product.running_stock.price -= discount;
                        product.discount = discount;
                }
               
            }
            db.Dispose();
            return product;
        }
        public ProductValues GetProductValues()
        {
            var prod = _context.products;

            int minStock = prod.Where(x => x.Quantity != 0).Count()>0? _context.products.Where(x => x.Quantity != 0).Min(x => x.Quantity):0;
            int maxStock = prod.Count()>0? _context.products.Max(q => q.Quantity):0;
            ProductValues p = new ProductValues();
            p.product_value = prod.Count()>0 ?prod.Sum(x => x.TotalValue):0;
            p.high_stock_product = prod.Where(x => x.Quantity ==maxStock).FirstOrDefault();
            p.low_stock_product = prod.Where(x => x.Quantity ==minStock).FirstOrDefault();
            p.number_of_Product = prod.Count(x => x.Quantity >0);
            p.quantity_of_products = prod.Count() > 0 ? prod.Sum(x => x.Quantity) : 0;
            p.out_of_stock_product = prod.Where(x => x.Quantity > 0).ToList();

            return p;
        }
        public async Task productOnsaleUpdate(int productid, string username,bool increase,int qnt)
        {
            var product_exist =  await _context.productOnsalelists.Where(x => x.username == username & x.ProductId == productid).FirstOrDefaultAsync();
            if (product_exist != null)
            {
                if (increase)
                {
                    product_exist.quantity += qnt;
                }
                else
                {
                    product_exist.quantity -= qnt;

                }
                 await updateProductOnSale(product_exist);
            }
            else
            {
                await productInListInitial(productid, username);
            }
        }
        public void ChangePrice(PriceChangeData new_price)
        {
            //get the current running stock for the specified product whose price is to be changed
            var current_running_stock = _context.stockTracks
                .Where(x => x.IsRunningStock == true & x.ProductId == new_price.product_id)
                .FirstOrDefault();

            //get all the stocks that are after the current stock [those are the available stocks] please not this is using FIFO
            var stocks_on_standby = _context.stockTracks
                .Where(x => x.ProductId == new_price.product_id & x.EditedOn <= current_running_stock.EditedOn)
                .OrderBy(x => x.EditedOn).ToArray();

            //sold products for today
            int products_already_sold = _context.sales.Where(x => x.ProductId == new_price.product_id &
                  DbFunctions.TruncateTime(x.DateOfSale) == DateTime.Now.Date & x.state == 1).Select(x => x.Quantity).Sum();

            //stock adjustment
            for (int i = 0; i < stocks_on_standby.Length; i++)
            {
                var stock = stocks_on_standby[i];
                if (stock.running_quantity >= products_already_sold)
                {
                    stock.running_quantity -= products_already_sold;
                    stock.quantity_sold += products_already_sold;
                    stock.adjusted = true;
                    stock.EditedOn = DateTime.Now;

                    StockTrack st = new StockTrack();
                    st.EditedBy = new_price.edited_by;
                    st.EditedOn = DateTime.Now;
                    st.BranchId = new_price.branch_id;
                    st.ProductId = new_price.product_id;
                    st.NewMarkup = new_price.new_markup;
                    st.PriceChange = new_price.new_price - stock.NewPrice;
                    st.NewQuantity = stock.running_quantity;
                    st.running_quantity = stock.running_quantity;
                    st.IsPriceChanged = true;
                    st.IsQuantintyChanged = products_already_sold == 0 ? false : true;
                    st.IsRunningStock = true;
                    st.NewValue = new_price.new_price * stock.NewQuantity;
                    st.NewPrice = new_price.new_price;
                    st.QuantityChange = stock.running_quantity;
                    st.quantity_sold = 0;

                    _stockTrack.updateStockTrack(stock);
                    _stockTrack.AddStockTrack(st);
                    break;
                }
                else
                {
                    products_already_sold -= stock.running_quantity;
                    stock.quantity_sold += stock.running_quantity;
                    stock.running_quantity = 0;
                    stock.adjusted = true;
                    stock.EditedOn = DateTime.Now;

                    _stockTrack.updateStockTrack(stock);
                }
            }

            //adjust unreached stocks
            var unadjusteed_stock = stocks_on_standby.Where(x => x.adjusted == false);
            foreach (var stock in unadjusteed_stock)
            {
                stock.NewMarkup = new_price.new_markup;
                stock.PriceChange = new_price.new_price - stock.NewPrice;
                stock.NewPrice = new_price.new_price;
                stock.IsPriceChanged = true;
                stock.EditedOn = DateTime.Now;

                _stockTrack.updateStockTrack(stock);
            }

        }
        #region private methods
        private async Task  productInListInitial(int productId, string username)
        {
            productOnsalelist pr = new productOnsalelist();
            pr.ProductId = productId;
            pr.username = username;
            pr.date = DateTime.Now;
            pr.quantity = 1;
             _context.productOnsalelists.Add(pr);
            await _context.SaveChangesAsync();
        }
        private void updateBlances(Product product)
        {
            OpenCloseBalances opb = new OpenCloseBalances();
            opb.ProductId = product.ProductId;
            opb.opening_balance = product.TotalValue;
            opb.opening_qnt = product.Quantity;
            opb.closing_balance = 0;
            opb.closing_qnt = 0;

            _context.openCloseBalances.Add(opb);
             _context.SaveChanges();
        }
        private async Task updateProductOnSale(productOnsalelist trans)
        {
            _context.Set<productOnsalelist>().AddOrUpdate( trans);
            await _context.SaveChangesAsync();
        }

     


        private void setZero(ProductSearchFilter f)
        {
            f.maxPrice = f.maxPrice == 0 ? decimal.MaxValue : f.maxPrice;
            f.maxQuantity = f.maxQuantity == 0 ? int.MaxValue : f.maxQuantity;
            f.maxTotalValue = f.maxTotalValue == 0 ? decimal.MaxValue : f.maxTotalValue;
        }

        #endregion

    }
}
