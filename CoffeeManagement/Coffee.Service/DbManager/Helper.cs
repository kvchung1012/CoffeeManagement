using Coffee.Core.BaseModel;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Coffee.Core.DbManager
{
    public static class Helper
    {
        public  static DynamicParameters AddBaseParam(this DynamicParameters par, BaseParamModel baseParam)
        {
            par.Add("@TableConfigName", baseParam.TableConfigName);
            par.Add("@FilterString", baseParam.FilterString);
            par.Add("@PageSize", baseParam.PageSize);
            par.Add("@PageNumber", baseParam.PageNumber);
            par.Add("@OrderBy", "");
            par.Add("@TotalCount", 0, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
            return par;
        } 
    }
}
