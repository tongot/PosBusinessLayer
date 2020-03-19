using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDatabase
{
    public class permissionToRoles
    {
        public int permissionToRolesId { get; set; }
        public  int RoleId { get; set; }
        public int PermissionsId { get; set; }

        public Permissions permissions { get; set; }
        public Role role { get; set; }

    }
}
