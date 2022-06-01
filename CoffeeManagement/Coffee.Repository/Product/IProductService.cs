using Coffee.Application.Common.Dtos;
using Coffee.Application.Product.Dto;
using Coffee.Core.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application
{
    public interface IProductService
    {
        public Task<ListResult<ProductDto>> GetListProduct(BaseParamModel baseParam);
        public Task<long> CreateOrUpdateProduct(ProductCreateDto product);
        public Task<ProductCreateDto> GetProductById(long id);
        public Task<int> Delete(long Id);
        public Task<List<SelectBoxDataDto>> GetListCombo();
        public Task<List<SelectBoxDataDto>> GetListSelectBox(int Id);

        public Task<List<ProductDto>> GetTopProduct();
    }
}
