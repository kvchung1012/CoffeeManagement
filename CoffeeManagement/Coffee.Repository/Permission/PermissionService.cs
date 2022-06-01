using Coffee.Application.Permission.Dto;
using Coffee.Core.DbManager;
using Dapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application
{
    public class PermissionService : IPermissionService
    {
        private readonly IDbManager _db;
        private readonly IHttpContextAccessor _httpContext;

        public PermissionService(IDbManager db, IHttpContextAccessor httpContext)
        {
            _db = db;
            _httpContext = httpContext;
        }

        public async Task<int> CreatePermissionRole(List<CreatePermissionRole> permissionRoles)
        {
            var con = _db.GetConnection;
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            using (var transaction = con.BeginTransaction())
            {
                try
                {
                    var parDel = new DynamicParameters();
                    parDel.Add("@RoleId", permissionRoles.FirstOrDefault()==null?0:permissionRoles.FirstOrDefault().RoleId);
                    var res = await _db.ExecuteAsync("Sp_Del_PermissionRole", parDel, transaction);

                    foreach (var item in permissionRoles)
                    {
                        var par = new DynamicParameters();
                        par.Add("@RoleId", item.RoleId);
                        par.Add("@PermissionId", item.PermissonId);
                        res = await _db.ExecuteAsync("Sp_Create_CreatePermissionRole", par, transaction);
                    }
                    transaction.Commit();
                    return res;
                }
                catch(Exception e)
                {
                    transaction.Rollback();
                    return 0;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public async Task<long> CreateRole(RoleDto role)
        {
            var param = new DynamicParameters();
            param.AddOutputId(role.Id);
            param.Add("@Name", role.Name);
            param.Add("@Description", role.Description);
            param.Add("@IsDefault", role.IsDefault);
            param.AddCreatedByDefault(_httpContext);
            var result = await _db.QueryAsync<RoleDto>("Sp_Create_Role", param);
            return param.GetOutputId();
        }

        public async Task<List<RoleDto>> GetAllRole()
        {
            var param = new DynamicParameters();
            var result = await _db.QueryAsync<RoleDto>("Sp_Get_GetAllRoles", param);
            return result;
        }

        public async Task<List<PermissionDto>> GetPermissionByRole(long Id)
        {
            var param = new DynamicParameters();
            param.Add("@RoleId", Id);
            var result = await _db.QueryAsync<PermissionDto>("Sp_Get_GetPermissionByRoles", param);
            return result;
        }
    }
}
