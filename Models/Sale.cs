using System;

namespace AppDatabase
{
    public class Sale
    {
        public int SaleId { get; set; }
        public string Ref  { get; set; }
        public int? CustomerId { get; set; }
        public int ProductId { get; set; }
        public Guid stock_id { get; set; }
        public int EmployeeId { get; set; }
     //   public int BranchId { get; set; }
        public DateTime DateOfSale { get; set; }
        public string customer { get; set; }
        public string employeeUsername { get; set; }
        public string productCode { get; set; }
        public decimal Price { get; set; }
        public decimal discount { get; set; }
        public int Quantity { get; set; }
        public DateTime runningDate { get; set; }
        public int state { get; set; }
        public string productName { get; set; }
        public decimal cashReceived { get; set; }
        public decimal change { get; set; }
        public string comment { get; set; }
        public decimal TotalPrice { get; set; }
        public string Branch { get; set; }
        public decimal totalTax { get; set; }
        public Product product { get; set; }
    }
}
