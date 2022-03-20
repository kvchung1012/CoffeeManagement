using Coffee.Application.Auth.Dtos;
using Coffee.Core.DbManager;
using Coffee.EntityFramworkCore.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IDbManager _db;
        public AuthService(IDbManager db)
        {
            _db = db;
        }
        public async Task<EntityFramworkCore.Model.Users> GetUserByUserName(UserLoginDto input)
        {
            var param = new DynamicParameters();
            param.Add("@UserName", input.UserName, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            var user = await _db.QueryFirstOrDefaultAsync<EntityFramworkCore.Model.Users>("sp_auth_getUserByUserName", param);
            return user;
        }
    }
}
