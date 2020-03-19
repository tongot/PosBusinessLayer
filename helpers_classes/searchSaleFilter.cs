using System;


namespace AppDatabase
{
    public class searchSaleFilter
    {
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string productCode { get; set; }
        public string refNumber { get; set; }
        public string  branchName { get; set; }
        public string  productName { get; set; }
        public string customerId { get; set; }
        public Category  category{ get; set; }
        public string employee_username { get; set; }
       
    }
    public enum periodOfSales
        {
            daily,
            monthly,
            yearly
        }
}
