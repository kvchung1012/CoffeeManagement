using Coffee.EntityFramworkCore.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.EntityFramworkCore.Model
{
    public class PermissionRole : BaseEntity
    {
        public long RoleId { get; set; }
        public long PermissionId { get; set; }
    }
}
