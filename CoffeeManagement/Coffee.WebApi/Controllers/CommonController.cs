using Coffee.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Coffee.WebApi.Controllers
{
    public class CommonController : BaseController
    {
        public ILogger<CommonController> _logger;
        private readonly ICommonService _commonService;
        private IHostingEnvironment _hostingEnvironment;
        public CommonController(ILogger<CommonController> logger
                                , ICommonService commonService
                                , IHostingEnvironment hostingEnvironment)
        {   
            _logger = logger;
            _commonService = commonService;
            _hostingEnvironment = hostingEnvironment;
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

        [HttpGet("{table}")]
        public async Task<IActionResult> GetSelectBoxData(string table)
        {
            var result = await _commonService.GetSelectBoxData(table);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFileImage()
        {
            try
            {
                var upload = HttpContext.Request.Form.Files[0];
                var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + upload.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), _hostingEnvironment.WebRootPath, "Upload/Image", fileName);
                var stream = new FileStream(path, FileMode.Create);
                await upload.CopyToAsync(stream);
                return Ok(new { uploaded = true, url = "/Upload/Image/" + fileName });
            }
            catch (Exception)
            {
                return Ok(new { uploaded = false });
            }
        }
    }
}
