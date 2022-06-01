using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application.Permission.Dto
{
    public class RoleDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
    }

    public class PermissionDto
    {
        public long RoleId { get; set; }
        public long PermissionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long ParentId { get; set; }
        public bool Checked { get; set; }
    }

    public class CreatePermissionRole
    {
        public long RoleId { get; set; }
        public long PermissonId { get; set; }
    }
}
