
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace AppDatabase
{
    public class EmployeeApp : IEmployee
    {
        ApplicationContext _context = new ApplicationContext();
        IRole role = new RoleApp();

        public void addEmployee(Employee employee)
        {
            //hash the password
            employee.Password = Encrypt(employee.Password);
            employee.JoiningDate = DateTime.Now;

               _context.employees.Add(employee);
             _context.SaveChanges();
        }
   
        /// <summary>
        /// return the user cridantials including roles
        /// </summary>
        /// <param name="password">hashed password from the application</param>
        /// <param name="username">user email of the user</param>
        /// <returns></returns>
        public userCredentials AuthnticateUser(string password, string username)
        {
            password = Encrypt(password);
            userCredentials credentials = new userCredentials();
            //check if user exist 
            if(_context.employees.Where(x=>x.username==username).FirstOrDefault()==null)
            {
                credentials.stateError = "Password or username Incorrect";
                return credentials;
            }
            //get the user to authanticate
            Employee emplo =_context.employees.Where(x => x.username == username & x.Password == password).FirstOrDefault();
            //check if user exist
            if(emplo == null)
            {
                credentials.stateError = "Incorrect username or password";
                return credentials;
            }
            credentials.roles = role.getUserRoles(emplo.EmployeeId).Select(x => x.Name).ToList();
            credentials.username = emplo.username;
            credentials.EmployeeId = emplo.EmployeeId;
            credentials.branch_name = emplo.Branch;
            credentials.branch_id = emplo.BranchId.Value;
            credentials.running_date = _context.COB_Runs.Where(x => x.IsCurrentDate).FirstOrDefault().RunningDate;

            emplo.IsLoggedIn = true;
            emplo.LastLogInTime = DateTime.Now;

            updateEmployee(emplo);
            return credentials;

            
        }
        public void logOutUser(string username)
        {
            Employee emplo = _context.employees.Where(x => x.username == username).FirstOrDefault();
            emplo.IsLoggedIn = false;
            emplo.LastLogOutTime = DateTime.Now;
            updateEmployee(emplo);
        }

        public async Task deleteEmployee(int? id)
        {
            if (id != null)
            {
                Employee emp = _context.employees.Find(id);
                _context.employees.Remove(emp);
                 _context.SaveChanges();
            }
        }

        public List<RoleToEmployee> employeeRoles(int id)
        {
            return _context.RoleToEmployees.Where(x => x.EmployeeId == id).ToList();
        }

        public Employee getEmployeeById(int id)
        {
            return _context.employees.Find(id);
        }

        public List<Employee> listEmployee(string searchBy, string searchString)
        {
            if (searchBy != null)
            {
                if (searchBy == "Name")
                {
                    return _context.employees.Where(x => x.Name.Contains(searchString)).ToList();
                }
                return _context.employees.Where(x => x.Name.Contains(searchString)).ToList();
            }
            return _context.employees.ToList();
        }

        public void updateEmployee(Employee employee)
        {
            _context.Set<Employee>().AddOrUpdate(employee);
            //_context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public string Encrypt(string value)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding uTF8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(uTF8.GetBytes(value));
                return Convert.ToBase64String(data);
            }
        }

        public List<Employee> getAllEmployees(string name)
        {
            return   _context.employees.Where(x=>x.Name.Contains(name)||name==null).ToList();
        }

        public bool userNameExist(string username)
        {
            var exist = _context.employees.Where(x => x.username == username).FirstOrDefault();
            return exist == null ? false : true;
        }
        public bool authenticateAdmin(string username, string password)
        {
            password = Encrypt(password);
            var emplo = _context.employees.Where(x => x.username == username && x.Password == password).FirstOrDefault();
            //check if user exist 
            if (emplo== null)
            {
                return false;
            }
            var admin_role = role.getUserRoles(emplo.EmployeeId).Select(x => x.Name);
            if(admin_role.Contains("Admin"))
            {
                return true;
            }
            return false;
        }
    }
}
