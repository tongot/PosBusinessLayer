using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDatabase
{
    public class StockTrack
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]

        public Guid StockTrackId { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedOn { get; set; }
        public decimal NewValue { get; set; }
        public decimal NewPrice { get; set; }
        public decimal PriceChange { get; set; }
        public int NewQuantity { get; set; }
        public decimal NewMarkup { get; set; }
        public int QuantityChange { get; set; }
        public int ProductId { get; set; }
        public int BranchId { get; set; }
        public bool IsPriceChanged { get; set; }
        public bool IsQuantintyChanged { get; set; }
        public bool IsRunningStock { get; set; }
        public int running_quantity { get; set; }
        public int quantity_sold { get; set; }
        public DateTime? CobDate { get; set; }

        [NotMapped]
        public bool adjusted { get; set; } = false;


    }
}
