using Coffee.Core.Auth;
using Coffee.Core.BaseModel;
using Dapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Coffee.Core.DbManager
{
    public static class Helper
    {
        public static DynamicParameters AddBaseParam(this DynamicParameters par, BaseParamModel baseParam)
        {
            par.Add("@TableConfigName", baseParam.TableConfigName);
            par.Add("@FilterString", baseParam.FilterString);
            par.Add("@PageSize", baseParam.PageSize);
            par.Add("@PageNumber", baseParam.PageNumber);
            par.Add("@OrderBy", baseParam.OrderBy);
            par.Add("@TotalCount", 0, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
            return par;
        }

        public static DynamicParameters AddOutputId(this DynamicParameters par, long Id)
        {
            par.Add("@Id", Id, System.Data.DbType.Int64, System.Data.ParameterDirection.InputOutput);
            return par;
        }

        public static long GetOutputId(this DynamicParameters par)
        {
            return par.Get<long>("@Id");
        }

        public static DynamicParameters AddCreatedByDefault(this DynamicParameters par, IHttpContextAccessor _httpContextAccessor)
        {
            try
            {
                par.Add("@CreatedBy", ((IdentityModel)_httpContextAccessor.HttpContext.User.Identity).Id);
                par.Add("@UpdatedBy", ((IdentityModel)_httpContextAccessor.HttpContext.User.Identity).Id);
            }
            catch
            {
                par.Add("@CreatedBy", 0);
                par.Add("@UpdatedBy", 0);
            }
            return par;
        }
    }
}
