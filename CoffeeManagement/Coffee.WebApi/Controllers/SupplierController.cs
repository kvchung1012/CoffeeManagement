using Coffee.Application;
using Coffee.Application.Suppiler.Dto;
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

        [HttpPost]
        public async Task<IActionResult> CreateUpdateSupplier(SupplierDto supplier)
        {
            var result = await _supplierService.CreateOrUpdateSuppiler(supplier);
            return Ok(result > 0);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(long Id)
        {
            var result = await _supplierService.Delete(Id);
            return Ok(result > 0);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _supplierService.GetAll();
            return Ok(result);
        }
    }
}
