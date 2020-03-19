

using System.Collections.Generic;
using System.Threading.Tasks;
namespace AppDatabase
{
   public  interface IEmployee
    {
        void addEmployee(Employee employee);
        Task deleteEmployee(int? id);
        void updateEmployee(Employee customer);
        List<RoleToEmployee> employeeRoles(int employeeId);
        Employee getEmployeeById(int id);
        List<Employee> listEmployee(string searchBy, string searchString);

        userCredentials AuthnticateUser(string password, string username);
        List<Employee> getAllEmployees(string name);
        string Encrypt(string value);

        bool userNameExist(string username);
        bool authenticateAdmin(string username, string password);
        void logOutUser(string username);
    }
}
