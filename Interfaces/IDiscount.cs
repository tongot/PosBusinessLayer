
using System.Collections.Generic;

namespace AppDatabase
{
   public  interface IDiscount
    {
        void AddDiscount(Discount discount);
        IEnumerable<Discount> GetNotGlobalDiscount();
        IEnumerable<Discount> GetDiscounts(string name);
        IEnumerable<Product> ProductsWithDiscount(int DiscountId);
        void DeleteDiscount(int DiscountId);
        void RemoveProductFromDiscount(int ProductId);
        void UpadateDiscount(Discount discount);
        decimal calculateDiscountForProduct(int productId, decimal price);
    }
}
