using Coffee.Application.Common.Dtos;
using Coffee.Core.BaseModel;
using Coffee.EntityFramworkCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application
{
    public interface ICommonService
    {
        Task<List<SystemTableColumn>> GetTableColumns(string tableName);
        Task<String> GetFilterString(BaseParamModel baseParamModel);
        Task<String> GetOrderBy(BaseParamModel baseParamModel);
        Task<List<SystemTableColumnDto>> GetTableColumnFilter(string tableName);
        Task<List<SelectBoxDataDto>> GetSelectBoxMasterData(long Id);
        Task<List<SelectBoxDataDto>> GetSelectBoxData(string table);

    }
}
