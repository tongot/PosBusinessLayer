using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
namespace AppDatabase
{
    public class RoleApp : IRole
    {
        ApplicationContext _context = new ApplicationContext();

        public void addRole(Role role)
        {
            _context.roles
                .Add(role);
             _context.SaveChanges();
        }

        public void deleteRole(int? id)
        {
            if (id != null)
            {
                //delete all role assigned on this role to be deleted
                foreach (var item in _context.RoleToEmployees)
                {
                    RoleToEmployee rol =  _context.RoleToEmployees.Find(item.RoleId);
                    if(rol !=null)
                    {
                         _context.RoleToEmployees.Remove(rol);
                    }
                   
                }
                foreach (var item in _context.permissionToRoles)
                {
                    permissionToRoles pmr = _context.permissionToRoles.Find(item.RoleId);
                    if(pmr != null)
                    {
                            _context.permissionToRoles.Remove(pmr);
                    }
                    
                }
                //delete the role
                Role role =  _context.roles.Find(id);
                if(role!= null)
                {
     _context.roles.Remove(role);
                 _context.SaveChanges();
                }
           
            }
        }

        public List<Role> getUserRoles(int id)
        {
            List<Role> roles= new List<Role>();
            roles = _context.RoleToEmployees.Where(x => x.EmployeeId == id).Select(x=>x.role).ToList();
            return roles;
        }

        public bool roleExist(string name)
        {
            Role r = _context.roles.Where(x => x.Name == name).FirstOrDefault();
            if(r!=null)
            {
                return true;
            }
            return false;
        }

        public List<Role> roles(string name)
        {
            return _context.roles.Where(x=>x.Name.Contains(name)||name==null).ToList();
        }

        public void updateRole(Role role)
        {
            _context.Set<Role>().AddOrUpdate(role);
             _context.SaveChanges();
        }
    }
}
