using Coffee.Application.Order.Dto;
using Coffee.Core.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application
{
    public interface IOrderService
    {
        public Task<ListResult<OrderDto>> GetListOrder(BaseParamModel baseParam);
        public Task<List<OrderDetailDto>> GetListOrderDetail(long Id);
        public Task<OrderFullDto> GetListOrderById(long Id);
        public Task<long> CreateCart(CreateCart input);
    }
}
