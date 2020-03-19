using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDatabase
{
    public class TaxApp : ITax
    {
        private ApplicationContext _context = new ApplicationContext();
        /// <summary>
        /// add a new discount
        /// </summary>
        /// <param name="discount"></param>
        public void AddTax(Tax Tax)
        {
            try
            {
                _context.Taxes.Add(Tax);
                _context.SaveChanges();
            }
            catch (Exception)
            {

            }

        }

        public decimal calculateTaxForProduct(int productId,decimal price)
        {
            decimal Tax = 0;
            var taxes = _context.productToTaxes.Where(x => x.ProductId == productId);
            if(taxes.Count()>0)
            {
              foreach (var item in taxes)
                        {
                          Tax += calcPriceWithTax(item.Tax.Percentage);
                        }
            }
            //get the global tax
            var globalTax = _context.Taxes.Where(x => x.TypeOfTex == TypeOfTax.Global);
            if (globalTax.Count() > 0)
            {
                foreach (var item in globalTax)
                {
                    Tax += calcPriceWithTax(item.Percentage);
                }
            }
            
            decimal calcPriceWithTax(decimal percentage)
            {
                return Convert.ToDecimal(percentage/ 100)*price;
            }
            return Tax;
        }

        /// <summary>
        /// delete a Tax record
        /// first deletes  the product relationship then delete itself
        /// </summary>
        /// <param name="TaxId">tax id</param>
        public void DeleteTax(int TaxId)
        {
            var tax = _context.productToTaxes.Where(x => x.TaxId == TaxId);
            if (tax.Count() > 0)
            {
                foreach (var item in tax)
                {
                    _context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }
            }
            var deleteTax = _context.Taxes.Find(TaxId);
            if (deleteTax != null)
            {
                _context.Taxes.Remove(deleteTax);
            }
            _context.SaveChanges();
        }
        /// <summary>
        /// get all texts that are not global
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Tax> GetNotGlobalText()
        {
            var taxes = _context.Taxes.Where(x => x.TypeOfTex == TypeOfTax.Special);
            return taxes;
        }

        /// <summary>
        /// get all taxes that are set
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Tax> GetTaxes(string name)
        {
            var tax = _context.Taxes.Where(x => x.Name.StartsWith(name) || name == null);
            return tax;
        }
        /// <summary>
        /// get all products associated with the given tax
        /// </summary>
        /// <param name="TaxId">tax Id</param>
        /// <returns></returns>
        public IEnumerable<Product> ProductsWithTax(int TaxId)
        {
            var products = _context.productToTaxes.Where(x => x.TaxId == TaxId).Select(x => x.Product);
            return products;
        }
        /// <summary>
        /// remove the product from considering the given tax
        /// </summary>
        /// <param name="taxId">product to remove</param>
        public void RemoveProductFromTax(int taxId)
        {
            var product = _context.productToTaxes.Where(x => x.ProductId == taxId).FirstOrDefault();
            if (product != null)
                _context.productToTaxes.Remove(product);

            _context.SaveChanges();
        }
   
        public void UpadateTax(Tax tax)
        {
            _context.Set<Tax>().AddOrUpdate(tax);
            _context.SaveChanges();
        }
    }
}
