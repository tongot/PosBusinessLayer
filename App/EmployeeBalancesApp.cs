using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDatabase
{
    public class EmployeeBalancesApp : IEmployeeBalances
    {
        ApplicationContext _context = new ApplicationContext();
        public void addNewBalance(EmployeeDailyBalances balances)
        {
            //check if the date is already in the table
            int count = _context.employeeDailyBalances.Where(x => DbFunctions.TruncateTime(x.date) == balances.date).Count();

            if (count==0)
            {
                _context.employeeDailyBalances.Add(balances);
                _context.SaveChanges();
            }
            else
            {
                updateSettlement(balances);
            }
        }
        
        public IEnumerable<EmployeeDailyBalances> getAllBalances()
        {
            var balances = _context.employeeDailyBalances;
            return balances;
        }

        public IEnumerable<EmployeeDailyBalances> getBalanceFor(int employee_id)
        {
            var balances = _context.employeeDailyBalances.Where(x=>x.EmployeeId==employee_id);
            return balances;
        }

        public EmployeeDailyBalances GetEmployeeDailyBalancesById(int id)
        {
            var record = _context.employeeDailyBalances.Find(id);
            return record;
        }
        public EmployeeDailyBalances getBalanceByDate(DateTime date)
        {
            var record = _context.employeeDailyBalances.Where(x=> DbFunctions.TruncateTime(x.date)==date.Date).FirstOrDefault();
            return record;
        }

        private bool updateSettlement(EmployeeDailyBalances record)
        {
            try
            {
                _context.Set<EmployeeDailyBalances>().AddOrUpdate(record);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }    
        }
    }
}
