using Coffee.Application.Users.Dtos;
using Coffee.Core.BaseModel;
using Coffee.Core.DbManager;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application.Users
{
    public class UserService : IUserService
    {
        private readonly IDbManager _db;
        public UserService(IDbManager db)
        {
            _db = db;
        }

        public async Task<(List<UserDtos>, int)> GetListUser(BaseParamModel baseParam)
        {
            var par = new DynamicParameters();
            par.AddBaseParam(baseParam);
            var res = await _db.QueryAsync<UserDtos>("Sp_Get_GetListUsers", par);
            var totalCount = par.Get<int>("@TotalCount");
            return (res, totalCount);
        }
    }
}
