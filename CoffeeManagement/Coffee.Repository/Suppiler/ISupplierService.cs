using Coffee.Application.Suppiler.Dto;
using Coffee.Core.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application
{
    public interface ISupplierService
    {
        public Task<ListResult<SupplierDto>> GetListSuppiler(BaseParamModel baseParam);
        public Task<int> CreateOrUpdateSuppiler(SupplierDto suppiler);
        public Task<int> Delete(long Id);
        public Task<List<SupplierDto>> GetAll();
    }
}
