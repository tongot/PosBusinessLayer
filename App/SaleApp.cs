

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Threading.Tasks;

namespace AppDatabase
{
    public class SaleApp : ISales
    {
        private ApplicationContext _context = new ApplicationContext();

        public async Task addSale(Sale sale)
        {
            _context.sales.Add(sale);
            await _context.SaveChangesAsync();
        }

        public async Task deleteSale(int? id)
        {
            if (id != null)
            {
                Product pr = await _context.products.FindAsync(id.Value);
                _context.products.Remove(pr);
                await _context.SaveChangesAsync();
            }
        }

        public Sale getSaleById(int id)
        {
            return _context.sales.Where(x => x.SaleId == id).FirstOrDefault(); 
        }

        public List<Sale> getSaleByRef(string Ref)
        {
            return _context.sales.Where(x => x.Ref == Ref).ToList() ;
        }

        public  SalesValues getAllSalesRevenue()
        {

            SalesValues s = new SalesValues();
            s.sales_revenue = sales(1).Count()>0? sales(1).Select(x => x.TotalPrice).Sum():0;
            s.failed_sales  = sales(2).Count() > 0 ? sales(2).Select(x => x.TotalPrice).Sum() : 0;
            s.void_sales = sales(3).Count() > 0 ? sales(3).Select(x => x.TotalPrice).Sum() : 0;
            s.sold_units = sales(1).Count() > 0 ? sales(1).Select(x => x.Quantity).Sum() : 0;

            return s;
        }
        private List<Sale> sales(int state)
        {
            return _context.sales.Where(x => x.state == state).ToList();
        }

        public List<Sale> listSale()
        {
            return _context.sales.ToList();
        }

        public List<Sale> sales(searchSaleFilter filter)
        {
            if (string.IsNullOrWhiteSpace(filter.productName))
            {
                filter.productName = null;
            }
            if (string.IsNullOrWhiteSpace(filter.productCode))
            {
                filter.productCode = null;
            }
            if (string.IsNullOrWhiteSpace(filter.refNumber))
            {
                filter.refNumber = null;
            }
            List<Sale> sales = new List<Sale>();
            sales = _context.sales.Where(x=>x.DateOfSale>filter.startDate.Value||filter.startDate==null)
                .Where(x => x.DateOfSale <= filter.endDate.Value || filter.endDate == null)
                .Where(x => x.productName.StartsWith(filter.productName) || filter.productName == null)
                .Where(x => x.productCode == filter.productCode || filter.productCode == null)
                .Where(x => x.customer.StartsWith(filter.customerId) || filter.customerId == null)
                .Where(x => x.Ref == filter.refNumber || filter.refNumber == null).ToList();

            return sales;
        }

        public async Task updateSaleState(Sale sale)
        {
            _context.Set<Sale>().AddOrUpdate(sale);
            await _context.SaveChangesAsync();
        }

        public DateSalesValues getMonthSales(searchSaleFilter filter, periodOfSales period)
        {

            if (filter.startDate == null)
            {
                filter.startDate = DateTime.Now;
            }


            if (period == periodOfSales.daily)
            {
                if (filter.endDate != null)
                {
                    //make sure to only get data for 60 days
                    TimeSpan time = filter.startDate.Value - filter.endDate.Value;
                    int days = time.Days;
                    if (days > 60)
                    {
                        filter.endDate = filter.startDate.Value.AddDays(60);
                    }
                }
                if (filter.endDate == null)
                {
                    filter.endDate = filter.startDate.Value.AddDays(30);
                }
            }

            DateSalesValues values = new DateSalesValues();
            values.sales_revenue = new List<decimal>();
            values.date = new List<string>();

            values.product_name = new List<string>();
            values.products_sold = new List<int>();

            //get sales after filter adjustment
            List<Sale> sales = new List<Sale>(period_sales(filter));


            //---group by daily--------
            if (period == periodOfSales.daily)
            {

                foreach (var item in sales)
                {
                    values.number_of_products += item.Quantity;
                    if (values.date.Count() > 0)
                    {
                        var sale_already_inlist = values.date.Last();


                        if (sale_already_inlist == item.DateOfSale.Date.ToShortDateString())
                        {
                            values.sales_revenue[values.date.IndexOf(sale_already_inlist)] += item.TotalPrice;
                        }
                        else
                        {
                            values.sales_revenue.Add(item.TotalPrice);
                            values.date.Add(item.DateOfSale.Date.ToShortDateString());


                        }
                    }
                    else
                    {
                        values.sales_revenue.Add(item.TotalPrice);
                        values.date.Add(item.DateOfSale.Date.ToShortDateString());
                    }

                }
                values.total_sales_revenue = values.sales_revenue.Sum();
            }
            //----group for monthly----
            else if (period == periodOfSales.monthly)
            {

                foreach (var item in sales)
                {
                    values.number_of_products += item.Quantity;
                    if (values.date.Count() > 0)
                    {
                        var sale_already_inlist = values.date.Last();
                        if (sale_already_inlist == item.DateOfSale.ToString("MMM"))
                        {
                            values.sales_revenue[values.date.IndexOf(sale_already_inlist)] += item.TotalPrice;
                        }
                        else
                        {
                            values.sales_revenue.Add(item.TotalPrice);
                            values.date.Add(item.DateOfSale.ToString("MMM"));
                        }
                    }
                    else
                    {

                        values.sales_revenue.Add(item.TotalPrice);
                        values.date.Add(item.DateOfSale.ToString("MMM"));
                    }

                }
                values.total_sales_revenue = values.sales_revenue.Sum();
            }
            //----group for yearly-----
            else
            {
                foreach (var item in sales)
                {
                    values.number_of_products += item.Quantity;
                    if (values.date.Count() > 0)
                    {
                        var sale_already_inlist = values.date.Last();
                        if (sale_already_inlist == item.DateOfSale.ToString("yyyy"))
                        {
                            values.sales_revenue[values.date.IndexOf(sale_already_inlist)] += item.TotalPrice;
                        }
                        else
                        {
                            values.sales_revenue.Add(item.TotalPrice);
                            values.date.Add(item.DateOfSale.ToString("yyyy"));
                        }
                    }
                    else
                    {

                        values.sales_revenue.Add(item.TotalPrice);
                        values.date.Add(item.DateOfSale.ToString("yyyy"));
                    }

                }
                values.total_sales_revenue = values.sales_revenue.Sum();
            }
            //product break-down by volumes
            foreach (var item in sales)
            {
                if (values.product_name.Count() > 0)
                {
                    if (values.product_name.Contains(item.productName))
                    {
                        values.products_sold[values.product_name.IndexOf(item.productName)] += item.Quantity;
                    }
                    else
                    {
                        values.product_name.Add(item.productName);
                        values.products_sold.Add(item.Quantity);
                    }
                }
                else
                {
                    values.product_name.Add(item.productName);
                    values.products_sold.Add(item.Quantity);
                }
            }
            return values;
        }
        public EmployeeDaySales employeeDayBalance(int tellerId)
        {
            EmployeeDaySales sale = new EmployeeDaySales();
            var date = DateTime.Now.Date;
            var sales = _context.sales.Where(x => x.EmployeeId == tellerId & DbFunctions.TruncateTime(x.DateOfSale) == date);
            foreach (var item in sales)
            {
                if (item.state == 1)
                {
                    sale.success_units += item.Quantity;
                    sale.success_value += item.TotalPrice;
                }
                else if (item.state == 2)
                {
                    sale.failed_units += item.Quantity;
                    sale.failed_value += item.TotalPrice;
                }
                else
                {
                    sale.void_units += item.Quantity;
                    sale.void_value += item.TotalPrice;
                }
            }
            return sale;
        }
        internal List<Sale> period_sales(searchSaleFilter filter)
        {
            List<Sale> sales = new List<Sale>();

            sales = _context.sales.Where(x => x.DateOfSale >= filter.startDate.Value || filter.startDate == null)
                .Where(x => x.state == 1)
                .Where(x => x.DateOfSale <= filter.endDate.Value || filter.endDate == null)
                .Where(x => x.productCode == filter.productCode || filter.productCode == null)
                .Where(x => x.employeeUsername == filter.employee_username || filter.employee_username == null)
                .Where(x => x.Branch.Contains(filter.branchName) || filter.branchName == null)
                .ToList();

            return sales;
        }

    }
    }
