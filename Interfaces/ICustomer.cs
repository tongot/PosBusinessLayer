

using System.Collections.Generic;
using System.Threading.Tasks;
namespace AppDatabase
{
    public interface ICustomer
    {
        void addCustomer(Customer customer);
        void deleteCustomer(int? id);
        void updateCustomer(Customer customer);
        Customer getCustomerById(int id);
        List<Customer> listCustomers(string searchBy,string searchString);
        List<Sale> getSalesByCustomer(int CustomerId);
        IEnumerable<Customer> getAllCustomers(string name);
        IEnumerable<Customer> getCustomerByIdLike(string nationalid);
    }
}
