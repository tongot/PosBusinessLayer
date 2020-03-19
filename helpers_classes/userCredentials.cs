

using System;
using System.Collections.Generic;

namespace AppDatabase
{
    public class userCredentials
    {
        public string username { get; set; }
        public string  branch_name { get; set; }
        public int branch_id { get; set; }
        public DateTime  running_date { get; set; }
        public int EmployeeId { get; set; }
        public string stateError { get; set; }
        public List<string> roles { get; set; }
    }
}
