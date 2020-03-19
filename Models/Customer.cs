

using System;
using System.Collections.Generic;

namespace AppDatabase
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name  { get; set; }
        public string Surname { get; set; }
        public int? Age { get; set; }
        public string Address { get; set; }
        public string  PhoneNumber { get; set; }
        public DateTime DateJoined { get; set; }
        public string nationalId { get; set; }
        public string NextOfKinContact { get; set; }

        public string FullName
        {
            get
            {
                return Name + " " + Surname;
            }
        }
        //public string MyProperty { get; set; }
        public string NextOfKin { get; set; }
        public ICollection<Sale>   sales { get; set; }
    }
}
