

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace AppDatabase
{
    public interface IProduct
    {
        Task<Product> addProduct(Product product, string username);
        void deleteProduct(int? id);
        Task updateProduct(Product product);
        Product getProductById(int id);

        /// <summary>
        /// matches those products with a code given
        /// </summary>
        /// <param name="codeString">code string entered by user</param>
        /// <returns></returns>
        List<Product> CodeLikeProducts(string codeString);
        Product GetProductByCode(string code, int branch_id);
        int TotalQuantity(ProductSearchFilter filters);
        decimal TotalValue(ProductSearchFilter filters);
        /// <summary>
        /// get the product to be diplayed  
        /// </summary>
        /// <param name="PageNumber">number of page to show the details</param>
        /// <param name="ItemsPerPage">number of items to show per page</param>
        /// <param name="filters">the model with value to filter the desired search</param>
        /// <returns></returns>
        List<Product> GetAllProducts(int PageNumber, int ItemsPerPage, ProductSearchFilter filters);
        List<Sale> getSalesByProduct(int ProductId);
        int totalProducts(ProductSearchFilter filters);
        List<Product> GetFilteredProduct(ProductSearchFilter filters, int PageNumber, int ItemsPerPage);

        /// <summary>
        /// update the quantity on a sales based effect
        /// </summary>
        /// <param name="id">id of product</param>
        /// <param name="qnt">quantity of product</param>
        /// /// <param name="qnt">branch of which sale belong</param>
        /// <param name="action">decrease of increase quantity 1=increasing quantities 0=decrease quantity</param>
        /// <returns></returns>
         Task updateQuantity(int product_id, int Qnt, decimal value, bool isIncrease);
        void updateSaleGoods(int product_id, Guid stock_id, bool isIncrease);
        int getQuantity(int id);
        Task productOnsaleUpdate(int productid, string username, bool increase,int qnt);
        ProductValues GetProductValues();
        void ChangePrice(PriceChangeData new_price);

       

    }
}
