using System;
using System.Collections.Generic;

namespace AppDatabase
{
    public interface IRolesToEmployee
    {
        void deleteByRoles(int roleId);
        void deleteByEmployee(int EmployeeId);
        void addRoleToEmployee(List<RoleToEmployee> roleToEmployee);
    }
}
