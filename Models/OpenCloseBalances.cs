using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDatabase
{
    public class OpenCloseBalances
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid OpenCloseBalancesId{ get; set; }
        public DateTime date { get; set; }
        public int ProductId { get; set; }
        public int opening_qnt { get; set; }
        public decimal opening_balance { get; set; }
        public int closing_qnt { get; set; }
        public decimal closing_balance { get; set; }

        public Product product { get; set; }
    }
}
