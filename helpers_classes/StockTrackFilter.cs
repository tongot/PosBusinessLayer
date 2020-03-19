

using System;

namespace AppDatabase
{
    public class StockTrackFilter
    { 
        public string EditedBy { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public decimal? PriceChangeMin { get; set; }
        public decimal? PriceChangeMax { get; set; }
        public int? QuantityChangeMin { get; set; }
        public int? QuantityChangeMax { get; set; }
        public int? ProductId { get; set; }
        public int? BranchId { get; set; }
        public bool? IsPriceChanged { get; set; }
        public bool? IsQuantityChanged { get; set; }
    }
}
