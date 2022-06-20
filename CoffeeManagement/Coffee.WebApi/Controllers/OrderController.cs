using Coffee.Application;
using Coffee.Application.Order.Dto;
using Coffee.Core.BaseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using static Coffee.Core.Constant.Constant;

namespace Coffee.WebApi.Controllers
{
    public class OrderController : BaseController
    {
        public ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        private readonly ICommonService _commonService;
        public OrderController(ILogger<OrderController> logger
                                , IOrderService orderService
                                , ICommonService commonService)
        {
            _logger = logger;
            _orderService = orderService;
            _commonService = commonService;
        }

        [HttpPost]
        public async Task<IActionResult> GetListOrder(BaseParamModel baseParam)
        {
            baseParam.FilterString = await _commonService.GetFilterString(baseParam);
            baseParam.OrderBy = await _commonService.GetOrderBy(baseParam);
            var result = await _orderService.GetListOrder(baseParam);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetListOrderById(long Id)
        {
            var result = await _orderService.GetListOrderById(Id);
            result.OrderDetail = await _orderService.GetListOrderDetail(Id);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetListOrderDetail(long Id)
        {
            var result = await _orderService.GetListOrderDetail(Id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateCart input)
        {
            var result = await _orderService.CreateCart(input,CartStatus.Success);
            return Ok(result);
        }
    }
}
