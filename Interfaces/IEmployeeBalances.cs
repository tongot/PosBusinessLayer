using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDatabase
{
    public interface IEmployeeBalances
    {
        void addNewBalance(EmployeeDailyBalances balances);
        IEnumerable<EmployeeDailyBalances> getBalanceFor(int employee_id);
        IEnumerable<EmployeeDailyBalances> getAllBalances();
        EmployeeDailyBalances GetEmployeeDailyBalancesById(int id);
        EmployeeDailyBalances getBalanceByDate(DateTime date);
        

    }
}
    