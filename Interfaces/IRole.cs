
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDatabase
{
    public interface IRole
    {
        void addRole(Role category);
        void deleteRole(int? id);
        void updateRole(Role customer);
        List<Role> roles(string name);
        List<Role> getUserRoles(int id);
        bool roleExist(string name);
    }
}
