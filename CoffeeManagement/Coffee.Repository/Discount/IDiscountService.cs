using Coffee.Application.Discount.Dto;
using Coffee.Core.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application
{
    public interface IDiscountService
    {
        public Task<ListResult<DiscountDto>> GetListDiscount(BaseParamModel baseParam);
        public Task<List<ProductDiscountDto>> GetListProductDiscount(long productId,long discountId);
        public Task<long> CreateOrUpdateDiscount(CreateDiscountDto discount);
        public Task<int> Delete(long Id);
        public Task<int> CreateProductDiscount(ProductDiscountDto productDiscount);
        public Task<int> DeleteProductDiscount(long Id);
        public Task<CreateDiscountDto> GetDiscountById(long Id);
    }
}
