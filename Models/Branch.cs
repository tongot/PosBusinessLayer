
using System.Collections.Generic;

namespace AppDatabase
{
    public class Branch
    {
        public int BranchId { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string Manager { get; set; }
        public ICollection<Sale> sales { get; set; }
        public ICollection<Employee> employees { get; set; }
        public ICollection<Product> products { get; set; }
    }
}
