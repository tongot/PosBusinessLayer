using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDatabase
{
    public class PermissionApp : IPermissions
    {
        private readonly ApplicationContext _context = new ApplicationContext();
        public void setPermission(List<permissionToRoles> RolePermissions)
        {
            if(RolePermissions.Count()>0)
            {
     int id = RolePermissions.FirstOrDefault().RoleId;
                if (RolePermissions.Count()>0)
                {
                   foreach (var item in _context.permissionToRoles.Where(x=>x.RoleId==id))
                    {
                        _context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                    foreach (var item in RolePermissions)
                    {
                        _context.permissionToRoles.Add(item);
                    }
                    _context.SaveChanges();
                }
            }
           
              
        }

        public IEnumerable<permissionToRoles> getPermissionsForRole(int RoleId)
        {
            return _context.permissionToRoles.Where(x => x.RoleId == RoleId).ToList();
        }

        public IEnumerable<Permissions> Permissions()
        {
           return _context.Permissions.ToList();
        }

        public Permissions GetPermissions(int permissionId)
        {
            return _context.Permissions.Find(permissionId);
        }
    }
}
