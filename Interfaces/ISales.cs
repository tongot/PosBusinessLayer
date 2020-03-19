

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDatabase
{
    public interface ISales
    {
        Task addSale(Sale sale);
        Task deleteSale(int? id);
        Sale getSaleById(int id);
        List<Sale> listSale();
        Task updateSaleState(Sale sale);
        List<Sale> getSaleByRef(string Ref);
        List<Sale> sales(searchSaleFilter filter);
        SalesValues getAllSalesRevenue();

        EmployeeDaySales employeeDayBalance(int teller_id);
        DateSalesValues getMonthSales(searchSaleFilter filter,periodOfSales period);

    }
}
