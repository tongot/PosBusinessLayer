using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDatabase
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public bool isSelected { get; set; }
        public ICollection<RoleToEmployee> roleToEmployees { get; set; }
        public ICollection<permissionToRoles> Permissions { get; set; }
    }
}
