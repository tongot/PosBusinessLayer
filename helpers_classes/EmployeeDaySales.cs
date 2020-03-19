using System;


namespace AppDatabase
{
    public class EmployeeDaySales
    {
        public int failed_units { get; set; } = 0;
        public decimal failed_value { get; set; } = 0;
        public int success_units { get; set; } = 0;
        public decimal success_value { get; set; } = 0;
        public int void_units { get; set; } = 0;
        public decimal void_value { get; set; } = 0;
    }
}
