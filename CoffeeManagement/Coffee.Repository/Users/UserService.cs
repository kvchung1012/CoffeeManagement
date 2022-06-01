using Coffee.Application.Users.Dtos;
using Coffee.Core.BaseModel;
using Coffee.Core.DbManager;
using Coffee.Core.Helper;
using Dapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application
{
    public class UserService : IUserService
    {
        private readonly IDbManager _db;
        private readonly IHttpContextAccessor _httpContext;

        public UserService(IDbManager db, IHttpContextAccessor httpContext)
        {
            _db = db;
            _httpContext = httpContext;
        }

        public async Task<long> CreateUser(CreateUserDto userDto)
        {
            var con = _db.GetConnection;
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            using (var transaction = con.BeginTransaction())
            {
                try
                {
                    var par = new DynamicParameters();
                    
                    par.AddOutputId(userDto.Id);
                    par.Add("@FullName", userDto.FullName);
                    par.Add("@UserName", userDto.UserName);
                    if (userDto.Id == 0)
                    {
                        var hashed = BcryptHelper.GetPasswordHash(userDto.UserName);
                        par.Add("@HashedPassword", hashed);
                    }
                    par.Add("@Phone", userDto.Phone);
                    par.Add("@Email", userDto.Email);
                    par.Add("@Birthday", userDto.Birthday);
                    par.Add("@Address", userDto.Address);
                    par.Add("@Identity", null);
                    par.Add("@IsActive", userDto.IsActive);
                    par.Add("@Status", userDto.Status);
                    par.AddCreatedByDefault(_httpContext);
                    var res = await _db.ExecuteAsync("Sp_Create_User", par, transaction);
                    transaction.Commit();
                    return par.GetOutputId();
                }
                catch (Exception e)
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

        public async Task<long> CreateUserRole(List<CreateUserRole> userRoles)
        {
            var con = _db.GetConnection;
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            using (var transaction = con.BeginTransaction())
            {
                try
                {
                    var parDel = new DynamicParameters();
                    parDel.Add("@UserId", userRoles.FirstOrDefault() == null ? 0 : userRoles.FirstOrDefault().UserId);
                    var res = await _db.ExecuteAsync("sp_del_userRole", parDel, transaction);

                    foreach (var item in userRoles)
                    {
                        var par = new DynamicParameters();
                        par.Add("@UserId", item.UserId);
                        par.Add("@RoleId", item.RoleId);
                        res = await _db.ExecuteAsync("sp_create_userRole", par, transaction);
                    }
                    transaction.Commit();
                    return res;
                }
                catch (Exception e)
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

        public async Task<List<UserDtos>> GetAllStaff()
        {
            var par = new DynamicParameters();
            var res = await _db.QueryAsync<UserDtos>("Sp_Get_GetAllStaff", par);
            return res;
        }

        public async Task<List<UserRole>> GetListRoleByUser(long Id)
        {
            var par = new DynamicParameters();
            par.Add("@UserId", Id);
            var res = await _db.QueryAsync<UserRole>("Sp_Get_GetRoleByUser", par);
            return res;
        }

        public async Task<ListResult<UserDtos>> GetListUser(BaseParamModel baseParam)
        {
            var par = new DynamicParameters();
            par.AddBaseParam(baseParam);
            var res = await _db.QueryAsync<UserDtos>("Sp_Get_GetListUsers", par);
            var totalCount = par.Get<int>("@TotalCount");
            return new ListResult<UserDtos>(res, totalCount);
        }

        public async Task<List<PositionByUser>> GetPositionByUserId(long id)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            var res = await _db.QueryAsync<PositionByUser>("Sp_Get_GetPositionByUser", par);
            return res;
        }

        public async Task<UserDtos> GetUserById(long id)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            var res = await _db.QueryFirstOrDefaultAsync<UserDtos>("Sp_Get_GetUserById", par);
            return res;
        }
    }
}
