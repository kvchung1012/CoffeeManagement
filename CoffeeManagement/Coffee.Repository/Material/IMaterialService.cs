using Coffee.Application.Material.Dto;
using Coffee.Core.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application
{
    public interface IMaterialService
    {
        public Task<ListResult<MaterialDto>> GetListMaterial(BaseParamModel baseParam);
        public Task<int> CreateOrUpdateMaterial(MaterialDto material);
        public Task<int> Delete(long Id);
    }
}
