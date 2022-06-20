using Coffee.Application.SaleCode.Dto;
using Coffee.Core.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application
{
    public interface ISaleCodeService
    {
        public Task<ListResult<SaleCodeDto>> GetListSaleCode(BaseParamModel baseParam);
        public Task<long> CreateOrUpdateSaleCode(CreateSaleCodeDto saleCode);
        //public Task<int> Delete(long Id);
        //public Task<int> CreateProductDiscount(ProductDiscountDto productDiscount);
        //public Task<int> DeleteProductDiscount(long Id);
        public Task<SaleCodeDto> GetSaleCodeById(long Id);
        public Task<SaleCodeDto> CheckSaleCode(string code);
        public Task<bool> CheckSaleCodeCanUse(long userId,string saleCode);
    }
}
