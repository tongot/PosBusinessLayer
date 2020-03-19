using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDatabase
{
public    class Tax
    {

        public int TaxId{ get; set; }
        [MaxLength(45)]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        [Range(0,100)]
        public decimal Percentage { get; set; }
        public TypeOfTax TypeOfTex { get; set; }

    }
    public enum TypeOfTax
    {
        Global=0,
        Special=1
    }
}
