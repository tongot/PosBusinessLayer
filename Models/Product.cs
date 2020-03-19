using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDatabase
{
   public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        //public decimal Price { get; set; }
        public decimal TotalValue { get; set; }
        public DateTime DateOfLastUpdate { get; set; }
       // public decimal PurchasePrice { get; set; }
        public string Manufacturer { get; set; }
        [MaxLength(45)]
        [Index(IsUnique =true)]
        public string ProductCode { get; set; }
        public string CapturedBy { get; set; }
        public int EmployeedId { get; set; }
        public int CategoryId { get; set; }
       // public int BranchId { get; set; }
        //public decimal markup { get; set; }
        public Category category { get; set; }
        public string EmployeeUsername { get; set; }
        public string BranchName { get; set; }
        public ICollection<Sale> sales { get; set; }
        public int state { get; set; }
        [NotMapped]
        public decimal taxToPay { get; set; }
        [NotMapped]
        public decimal discount { get; set; }
        [NotMapped]
        public Stock running_stock { get; set; }


    }
}
