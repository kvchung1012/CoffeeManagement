using Coffee.Application;
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

        [HttpGet("{tableName}")]
        public async Task<IActionResult> GetListColumn(string tableName)
        {
            var result = await _commonService.GetTableColumns(tableName);
            return Ok(result);
        }

        [HttpGet("{tableName}")]
        public async Task<IActionResult> GetListColumnFilter(string tableName)
        {
            var result = await _commonService.GetTableColumnFilter(tableName);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetMasterDataByGroupId(long Id)
        {
            var result = await _commonService.GetSelectBoxMasterData(Id);
            return Ok(result);
        }
    }
}
