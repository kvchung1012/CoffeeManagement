using Coffee.Application.Permission.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application
{
    public interface IPermissionService
    {
        public Task<List<RoleDto>> GetAllRole();
        public Task<List<PermissionDto>> GetPermissionByRole(long Id);
        public Task<int> CreatePermissionRole(List<CreatePermissionRole> permissionRoles);
        public Task<long> CreateRole(RoleDto role);
    }
}
