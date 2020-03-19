
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDatabase
{
public    class Discount
    {
        public int DiscountId { get; set; }
        [MaxLength(45)]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        [Range(0, 100)]
        public decimal percentage { get; set; }
        public TypeOfDiscount TypeOfDiscount { get; set; }
    }
    public enum TypeOfDiscount
    {
        Global,
        Special
    }
}
