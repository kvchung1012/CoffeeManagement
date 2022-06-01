using Coffee.Application;
using Coffee.Application.WareHouse.Dto;
using Coffee.Core.BaseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee.WebApi.Controllers
{
    public class WareHouseController : BaseController
    {
        public ILogger<WareHouseController> _logger;
        private readonly IWareHouseService _wareHouseService;
        private readonly ICommonService _commonService;
        public WareHouseController(ILogger<WareHouseController> logger
                                , IWareHouseService wareHouseService
                                , ICommonService commonService)
        {
            _logger = logger;
            _wareHouseService = wareHouseService;
            _commonService = commonService;
        }

        [HttpPost]
        public async Task<IActionResult> GetListWareHouse(GetWareHouse input)
        {
            var res = await _wareHouseService.GetListWareHouse(input);
            return Ok(res);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetListWareHouseDetail(long Id)
        {
            var res = await _wareHouseService.GetListWareHouseDetail(Id);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExportInvoice(CreateExportInvoiceDto createExportInvoiceDto)
        {
            var result = await _wareHouseService.CreateExportInvoice(createExportInvoiceDto);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetListExportInvoice(BaseParamModel baseParam)
        {
            var result = await _wareHouseService.GetListExportInvoice(baseParam);
            return Ok(result);
        }
    }
}
