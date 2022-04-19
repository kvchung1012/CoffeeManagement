using Coffee.Application;
using Coffee.Core.BaseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Coffee.WebApi.Controllers
{
    public class SupplierController : BaseController
    {
        public ILogger<SupplierController> _logger;
        private readonly ISupplierService _supplierService;
        private readonly ICommonService _commonService;
        public SupplierController(ILogger<SupplierController> logger
                                , ISupplierService supplierService
                                , ICommonService commonService)
        {
            _logger = logger;
            _supplierService = supplierService;
            _commonService = commonService;
        }

        [HttpPost]
        public async Task<IActionResult> GetListSupplier(BaseParamModel baseParam)
        {
            baseParam.FilterString = await _commonService.GetFilterString(baseParam);
            baseParam.OrderBy = await _commonService.GetOrderBy(baseParam);
            var result = await _supplierService.GetListSuppiler(baseParam);
            return Ok(result);
        }
    }
}
