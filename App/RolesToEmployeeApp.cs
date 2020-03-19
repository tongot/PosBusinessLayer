

using System.Collections.Generic;
using System.Linq;

namespace AppDatabase
{
    public class RolesToEmployeeApp : IRolesToEmployee
    {
        ApplicationContext _context = new ApplicationContext();
       
            
    
        /// <summary>
        /// add roles
        /// </summary>
        /// <param name="roleToEmployee"></param>
        public void addRoleToEmployee(List<RoleToEmployee> roleToEmployee)
        {
            if (roleToEmployee.Count > 0)
            {
                foreach (var item in roleToEmployee)
                {
                      _context.RoleToEmployees.Add(item);
                }
                _context.SaveChanges();
            }
           
        }

        /// <summary>
        /// delete by employee 
        /// </summary>
        /// <param name="EmployeeId"></param>
        public void deleteByEmployee(int EmployeeId)
        {
            var roles = _context.RoleToEmployees.Where(x => x.EmployeeId == EmployeeId);
            if(roles.Count()>0)
            {
                foreach (var item in roles)
                {
                    _context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }
                _context.SaveChanges();
            }
        }
        /// <summary>
        /// delete by roles 
        /// </summary>
        /// <param name="roleId"></param>
        public void deleteByRoles(int roleId)
        {
            var roles = _context.RoleToEmployees.Where(x => x.RoleId == roleId);
            if (roles.Count() > 0)
            {
                foreach (var item in roles)
                {
                    _context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }
                _context.SaveChanges();
            }
        }
    }
}
