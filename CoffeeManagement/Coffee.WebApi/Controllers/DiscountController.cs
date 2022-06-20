using Coffee.Application;
using Coffee.Application.Common;
using Coffee.Application.Discount.Dto;
using Coffee.Core.BaseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Coffee.WebApi.Controllers
{
    public class DiscountController : BaseController
    {
        public ILogger<DiscountController> _logger;
        private readonly IDiscountService _discountService;
        private readonly ICommonService _commonService;
        public DiscountController(ILogger<DiscountController> logger
                                , IDiscountService discountService
                                , ICommonService commonService)
        {
            _logger = logger;
            _discountService = discountService;
            _commonService = commonService;
        }

        [HttpPost]
        public async Task<IActionResult> GetListDiscount(BaseParamModel baseParam)
        {
            baseParam.FilterString = await _commonService.GetFilterString(baseParam);
            baseParam.OrderBy = await _commonService.GetOrderBy(baseParam);
            var result = await _discountService.GetListDiscount(baseParam);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(CreateDiscountDto input)
        {
            var result = await _discountService.CreateOrUpdateDiscount(input);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetDiscountById(long Id)
        {
            var result = await _discountService.GetDiscountById(Id);
            return Ok(result);
        }
    }
}
