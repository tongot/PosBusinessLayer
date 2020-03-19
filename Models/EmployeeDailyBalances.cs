using System;
namespace AppDatabase
{
    public class EmployeeDailyBalances
    {
        public int EmployeeDailyBalancesId { get; set; }
        public DateTime date { get; set; }
        public decimal closing_balance { get; set; }
        public settled balanced_off { get; set; }
        public decimal balance_difference { get; set; }
        public bool settled { get; set; }
        public int EmployeeId { get; set; }
        public string comment { get; set; }
        public Employee Employee { get; set; }
        public DateTime? settlement_date { get; set; }
        public DateTime balance_off_date { get; set; }
        public int balanced_of_by { get; set; }
        public int? sattled_by { get; set; }
    }
    public enum settled
    {
        Yes,No,
    }
}
