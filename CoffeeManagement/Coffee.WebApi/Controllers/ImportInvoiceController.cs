using Coffee.Application;
using Coffee.Application.ImportInvoice.Dto;
using Coffee.Core.BaseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Coffee.WebApi.Controllers
{
    public class ImportInvoiceController : BaseController
    {
        public ILogger<ImportInvoiceController> _logger;
        private readonly IImportInvoiceService _importInvoiceService;
        private readonly ICommonService _commonService;
        public ImportInvoiceController(ILogger<ImportInvoiceController> logger
                                , IImportInvoiceService importInvoiceService
                                , ICommonService commonService)
        {
            _logger = logger;
            _importInvoiceService = importInvoiceService;
            _commonService = commonService;
        }

        [HttpPost]
        public async Task<IActionResult> GetListImportInvoice(BaseParamModel baseParamModel)
        {
            baseParamModel.FilterString = await _commonService.GetFilterString(baseParamModel);
            baseParamModel.OrderBy = await _commonService.GetOrderBy(baseParamModel);
            var result = await _importInvoiceService.GetListImportInvoice(baseParamModel);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateImportInvoice(CreateImportInvoice input)
        {
            var result = await _importInvoiceService.CreateImportInvoice(input);
            return Ok(result);
        }
    }
}
