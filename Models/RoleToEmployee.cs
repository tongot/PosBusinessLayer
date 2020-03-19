using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDatabase
{
    public class RoleToEmployee
    {
        public int RoleToEmployeeId { get; set; }
        public int RoleId{ get; set; }
        public int EmployeeId { get; set; }

        public Employee employee { get; set; }
        public Role role { get; set; }
    }
}
