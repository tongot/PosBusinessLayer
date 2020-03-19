

using System.Collections;
using System.Collections.Generic;

namespace AppDatabase
{
    public class Permissions
    {
        public int PermissionsId { get; set; }
        public string name { get; set; }

        public ICollection<permissionToRoles> Roles { get; set; }

    }
}
