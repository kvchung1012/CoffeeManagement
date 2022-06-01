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

namespace Coffee.Application
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
            if (baseParamModel.filterColumns == null) return "";
            else if (baseParamModel.filterColumns.Count() == 0) return "";

            string filter = String.Empty;
            foreach (var col in baseParamModel.filterColumns)
            {
                var syscol = res.Find(x => x.Id == col.ColumnId);
                if (syscol != null)
                {
                    // là kiểu chuỗi
                    if (syscol.DataTypeId == Constant.DataTypeColumn.String)
                    {
                        filter += $"AND {syscol.SqlAlias}.{syscol.SqlColumnName} like N'%{col.Value}%'";
                    }
                    else if(syscol.DataTypeId == Constant.DataTypeColumn.DateTime)
                    {
                        string shortDate = DateTime.Parse(col.Value).ToString("dd/MM/yyyy");
                        filter += $"AND CONVERT(VARCHAR(10), {syscol.SqlAlias}.{syscol.SqlColumnName}, 103) = N'{shortDate}'";
                    }
                    else if (syscol.DataTypeId == Constant.DataTypeColumn.Number || syscol.DataTypeId == Constant.DataTypeColumn.Select)
                    {
                        filter += $"AND {syscol.SqlAlias}.{syscol.SqlColumnName} = {col.Value}";
                    }
                    else if (syscol.DataTypeId == Constant.DataTypeColumn.SelectMultiple)
                    {
                        filter += $"AND {syscol.SqlAlias}.{syscol.SqlColumnName} in ({col.Value})";
                    }
                    else if(syscol.DataTypeId == Constant.DataTypeColumn.CheckBox)
                    {
                        var val = col.Value == "true"?1:0;
                        filter += $"AND {syscol.SqlAlias}.{syscol.SqlColumnName} = ({val})";
                    }
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

        public async Task<List<SystemTableColumnDto>> GetTableColumnFilter(string tableName)
        {
            var par = new DynamicParameters();
            par.Add("@TableName", tableName);
            var res = await _db.QueryAsync<SystemTableColumnDto>("Sp_System_GetTableColumnFilter", par);
            foreach (var item in res)
            {
                if (item.DataTypeId == Constant.DataTypeColumn.Select || item.DataTypeId == Constant.DataTypeColumn.SelectMultiple)
                {
                    if (!string.IsNullOrEmpty(item.QueryData))
                    {
                        var param = new DynamicParameters();
                        item.SelectBoxData = await _db.QueryAsync<SelectBoxDataDto>(item.QueryData, par, null, System.Data.CommandType.Text);
                    }
                }
            }
            return res;
        }

        public async Task<string> GetOrderBy(BaseParamModel baseParamModel)
        {
            var par = new DynamicParameters();
            par.Add("@TableName", baseParamModel.TableConfigName);
            var res = await _db.QueryAsync<SystemTableColumn>("Sp_System_GetTableColumn", par);
            var col = res.Find(x => x.Id == baseParamModel.SortBy);
            if (col != null)
            {
                return $"{col.SqlAlias}.{col.SqlColumnName} " + (baseParamModel.IsAsc ? "" : "desc");
            }
            else return "";
        }

        public async Task<List<SelectBoxDataDto>> GetSelectBoxMasterData(long Id)
        {
            var par = new DynamicParameters();
            par.Add("@Id", Id);
            var result = await _db.QueryAsync<SelectBoxDataDto>("Sp_Get_MasterDataByGroupId",par);
            return result;
        }

        public async Task<List<SelectBoxDataDto>> GetSelectBoxData(string table)
        {
            var par = new DynamicParameters();
            par.Add("@tableName", table);
            return await _db.QueryAsync<SelectBoxDataDto>("sp_get_selectbox",par );
        }
    }
}
