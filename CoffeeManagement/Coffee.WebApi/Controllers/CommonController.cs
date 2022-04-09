using Coffee.Application.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Coffee.WebApi.Controllers
{
    public class CommonController : BaseController
    {
        public ILogger<CommonController> _logger;
        private readonly ICommonService _commonService;
        public CommonController(ILogger<CommonController> logger
                                , ICommonService commonService)
        {
            _logger = logger;
            _commonService = commonService;
        }

        [HttpPost]
        public async Task<IActionResult> GetListColumn(string columnName)
        {
            var result = await _commonService.GetTableColumns(columnName);
            return Ok(result);
        }
    }
}
