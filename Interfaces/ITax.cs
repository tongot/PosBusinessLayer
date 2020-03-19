using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDatabase
{
    public interface ITax
    {
        void AddTax(Tax Tax);
        IEnumerable<Tax> GetNotGlobalText();
        IEnumerable<Tax> GetTaxes(string name);
        IEnumerable<Product> ProductsWithTax(int TaxID);
        void DeleteTax(int DiscountId);
        void RemoveProductFromTax(int TaxId);
        void UpadateTax(Tax Tax);
        decimal calculateTaxForProduct(int productId,decimal price);
    }
}
