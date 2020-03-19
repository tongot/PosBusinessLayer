using System;
using System.Collections.Generic;


namespace AppDatabase
{
    public interface IPermissions
    {
        void setPermission(List<permissionToRoles> RolePermissions);
        IEnumerable<Permissions> Permissions();
        IEnumerable<permissionToRoles> getPermissionsForRole(int RoleId);
        Permissions GetPermissions(int permissionId);

    }
}
