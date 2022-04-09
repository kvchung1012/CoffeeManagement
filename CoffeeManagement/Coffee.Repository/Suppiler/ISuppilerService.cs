using Coffee.Application.Suppiler.Dto;
using Coffee.Core.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application
{
    public interface ISuppilerService
    {
        public Task<ListResult<SuppilerDto>> GetListSuppiler(BaseParamModel baseParam);
        public Task<int> CreateOrUpdateSuppiler(SuppilerDto suppiler);
        public Task<int> Delete(long Id);
    }
}
