

namespace AppDatabase
{
    /// <summary>
    /// filter model to the database ... recieves data from the filter view model fro the application level
    /// </summary>
    public class ProductSearchFilter
    {
        public string searchString { get; set; }
        public decimal minPrice { get; set; } 
        public decimal maxPrice { get; set; } 
        public int minQuantity { get; set; } 
        public int maxQuantity { get; set; } 
        public decimal minTotalValue { get; set; } 
        public decimal maxTotalValue { get; set; } 
    }
}
