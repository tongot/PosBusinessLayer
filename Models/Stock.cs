using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AppDatabase
{
    public class Stock
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid stock_id { get; set; }
        public DateTime date_of_stock { get; set; }
        public int quantity { get; set;}
        public decimal purchase_price { get; set; }
        public decimal price { get; set;}
        public decimal total_value { get; set;}
        public decimal markup { get; set;}
        public int ProductId { get; set;}
        public int BranchId { get; set; }
        public int current_running_stock { get; set; }
        public decimal current_revenue { get; set; }
        public bool is_running_stock { get; set; }
        public string batch_number { get; set; }
        public virtual Branch Branch { get; set; }
        [NotMapped]
        public bool is_selected { get; set; }
    }
}
