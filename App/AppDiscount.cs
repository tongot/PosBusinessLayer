using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDatabase
{
    public class AppDiscount : IDiscount
    {
        private ApplicationContext _context = new ApplicationContext();
        /// <summary>
        /// add a new discount
        /// </summary>
        /// <param name="discount"></param>
        public void AddDiscount(Discount discount)
        {
            try
            {
                _context.Discounts.Add(discount);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                   
            }

        }
        public decimal calculateDiscountForProduct(int productId, decimal price)
        {
            decimal DIiscount = 0;
            var taxes = _context.productToDiscounts.Where(x => x.ProductId == productId);
            if (taxes.Count() > 0)
            {
                foreach (var item in taxes)
                {
                    DIiscount += calcPriceWithTax(item.Discount.percentage);
                }
            }
            //get the global tax
            var globalTax = _context.Discounts.Where(x => x.TypeOfDiscount == TypeOfDiscount.Global);
            if (globalTax.Count() > 0)
            {
                foreach (var item in globalTax)
                {
                    DIiscount += calcPriceWithTax(item.percentage);
                }
            }

            decimal calcPriceWithTax(decimal percentage)
            {
                return (Convert.ToDecimal(percentage / 100) * price);
            }
            return DIiscount;
        }
        /// <summary>
        /// delete a discount
        /// first deletes  the pruduct relationship then delete itself
        /// </summary>
        /// <param name="DiscountId">discount id</param>
        public void DeleteDiscount(int DiscountId)
        {
           var discount = _context.productToDiscounts.Where(x => x.DiscountId == DiscountId);
           if(discount.Count()>0)
            {
              foreach (var item in discount)
                        {
                            _context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                        }
            }
            var deleteDic = _context.Discounts.Find(DiscountId);
             if(deleteDic!=null)
            {
                _context.Discounts.Remove(deleteDic);
            }
            _context.SaveChanges();
        }
        /// <summary>
        /// get all discounts that are set
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Discount> GetDiscounts(string name)
        {
            var discounts = _context.Discounts.Where(x=>x.Name.StartsWith(name)||name==null);
            return discounts;
        }
        /// <summary>
        /// get all discounts that are not global
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Discount> GetNotGlobalDiscount()
        {
            var discount = _context.Discounts.Where(x => x.TypeOfDiscount == TypeOfDiscount.Special);
            return discount;
        }

        /// <summary>
        /// get all products associated with the given discount
        /// </summary>
        /// <param name="discountId">discount Id</param>
        /// <returns></returns>
        public IEnumerable<Product> ProductsWithDiscount(int discountId)
        {
            var products = _context.productToDiscounts.Where(x => x.DiscountId == discountId).Select(x => x.Product);
            return products;
        }
        /// <summary>
        /// remove the product from considering the given discount
        /// </summary>
        /// <param name="ProductId">product to remove</param>
        public void RemoveProductFromDiscount(int ProductId)
        {
            var product = _context.productToDiscounts.Where(x => x.ProductId == ProductId).FirstOrDefault();
            if (product != null)
                _context.productToDiscounts.Remove(product);

            _context.SaveChanges();
        }
        /// <summary>
        /// update the discount entity
        /// </summary>
        /// <param name="discount"></param>
        public void UpadateDiscount(Discount discount)
        {
            _context.Set<Discount>().AddOrUpdate(discount);
            _context.SaveChanges();
        }
    }
}
