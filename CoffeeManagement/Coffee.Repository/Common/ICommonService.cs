using Coffee.Application.Common.Dtos;
using Coffee.EntityFramworkCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application.Common
{
    public interface ICommonService
    {
        Task<List<SystemTableColumn>> GetTableColumns(string tableName);
        Task<String> GetFilterString(BaseParamModel baseParamModel);



    }
}
