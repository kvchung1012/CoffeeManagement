using Coffee.Application.Common.Dtos;
using Coffee.Core.BaseModel;
using Coffee.Core.Constant;
using Coffee.Core.DbManager;
using Coffee.EntityFramworkCore.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application.Common
{
    public class CommonService : ICommonService
    {
        private readonly IDbManager _db;
        public CommonService(IDbManager db)
        {
            _db = db;
        }

        public async Task<string> GetFilterString(BaseParamModel baseParamModel)
        {
            var par = new DynamicParameters();
            par.Add("@TableName", baseParamModel.TableConfigName);
            var res = await _db.QueryAsync<SystemTableColumn>("Sp_System_GetTableColumn", par);
            if (baseParamModel.filterColumns.Count() > 0) return "";

            string filter = String.Empty;
            foreach(var col in baseParamModel.filterColumns)
            {
                var syscol = res.Find(x => x.Id == col.ColumnId);
                if (syscol != null)
                {
                    if(syscol.DataTypeId == Constant.DataTypeColumn.String) // là kiểu chuỗi
                        filter += $"AND {syscol.SqlAlias}.{syscol.SqlColumnName} like N'%{col.Value}%";
                    else if(syscol.DataTypeId == Constant.DataTypeColumn.Number || syscol.DataTypeId == Constant.DataTypeColumn.DateTime || syscol.DataTypeId == Constant.DataTypeColumn.Select)
                        filter += $"AND {syscol.SqlAlias}.{syscol.SqlColumnName} = {col.Value}";
                    else if(syscol.DataTypeId == Constant.DataTypeColumn.SelectMultiple)
                        filter += $"AND {syscol.SqlAlias}.{syscol.SqlColumnName} in ({col.Value})";
                }
            }
            return filter;

        }

        public async Task<List<SystemTableColumn>> GetTableColumns(string tableName)
        {
            var par = new DynamicParameters();
            par.Add("@TableName", tableName);
            var res = await _db.QueryAsync<SystemTableColumn>("Sp_System_GetTableColumn", par);
            return res;
        }
    }
}
