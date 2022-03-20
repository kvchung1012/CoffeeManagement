using Coffee.Application.Common.Dtos;
using Coffee.Application.Users.Dtos;
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
            par.Add("@TableConfigName", baseParam.TableConfigName);
            par.Add("@FilterString", baseParam.FilterString);
            par.Add("@PageSize", baseParam.PageSize);
            par.Add("@PageNumber", baseParam.PageNumber);
            par.Add("@OrderBy", "");
            par.Add("@TotalCount", 0, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
            var res = await _db.QueryAsync<UserDtos>("Sp_Get_GetListUsers", par);
            var totalCount = par.Get<int>("@TotalCount");
            return (res, totalCount);
        }
    }
}
