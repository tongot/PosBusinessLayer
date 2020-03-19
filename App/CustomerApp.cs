using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
namespace AppDatabase
{
    public class CustomerApp:ICustomer
    {
        private ApplicationContext _context = new ApplicationContext();

        public void addCustomer( Customer customer)
        {
            _context.customers.Add(customer);
            _context.SaveChanges();
        }

        public void deleteCustomer(int? id)
        {
            if (id != null)
            {
                Customer cs =  _context.customers.Find(id.Value);
                _context.customers.Remove(cs);
                 _context.SaveChanges();
            }

        }

        public IEnumerable<Customer> getAllCustomers( string name)
        {
            return _context.customers.Where(x=>x.Name.Contains(name)||name==null).ToList();
        }

        public Customer getCustomerById(int id)
        {
                return  _context.customers.Where(x=>x.CustomerId==id).FirstOrDefault();
        }

        public IEnumerable<Customer> getCustomerByIdLike(string nationalid)
        {
            return _context.customers.Where(x => x.nationalId.Contains(nationalid)).ToList();
        }

        public List<Sale> getSalesByCustomer(int CustomerId)
        {
            List<Sale> sales = new List<Sale>();
            sales = _context.sales.Where(x => x.CustomerId == CustomerId).ToList();
            return sales;
        }

        public List<Customer> listCustomers(string searchBy, string searchString)
        {
            if(searchBy!=null)
            {
                if (searchBy == "Name")
                {
                    return _context.customers.Where(x => x.Name.Contains(searchString)).ToList();
                }
                return _context.customers.Where(x => x.PhoneNumber.Contains(searchString)).ToList();
            }
            return _context.customers.ToList();
        }

        public async Task updateCustomer(Customer customer)
        {
            _context.Entry(customer).State= EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        void ICustomer.updateCustomer(Customer customer)
        {
            _context.Set<Customer>().AddOrUpdate(customer);

            _context.SaveChanges();
        }
    }
}
