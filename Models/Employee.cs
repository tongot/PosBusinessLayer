using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDatabase
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmployeeNumber { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string NationalIdNumber { get; set; }
        public string Branch { get; set; }

        public int? BranchId { get; set; }
        public bool firsttimePassword { get; set; }
        public DateTime JoiningDate { get; set; }
        public string username { get; set; }
        public string FullName { get
            { return Name + " " + Surname;   }
            }
        public bool IsLoggedIn { get; set; }
        public DateTime? LastLogInTime { get; set; }
        public DateTime? LastLogOutTime { get; set; }
        public DateTime? passwordLastChangedOn { get; set; }

        public ICollection<Sale> sales { get; set; }
        public ICollection<RoleToEmployee> roleToEmployees { get; set;  }
        public ICollection<Product> products { get; set; }
    }
}
