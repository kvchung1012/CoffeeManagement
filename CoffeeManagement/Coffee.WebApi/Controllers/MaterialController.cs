﻿using Coffee.Application;
using Coffee.Application.Material.Dto;
using Coffee.Core.BaseModel;
using Coffee.WebApi.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Coffee.WebApi.Controllers
{
    [CustomAuthorize]
    public class MaterialController : BaseController
    {
        public ILogger<MaterialController> _logger;
        private readonly IMaterialService _materialService;
        private readonly ICommonService _commonService;
        public MaterialController(ILogger<MaterialController> logger
                                , IMaterialService materialService
                                , ICommonService commonService)
        {
            _logger = logger;
            _materialService = materialService;
            _commonService = commonService;
        }

        [HttpPost]
        public async Task<IActionResult> GetListMaterial(BaseParamModel baseParam)
        {
            baseParam.FilterString = await _commonService.GetFilterString(baseParam);
            baseParam.OrderBy = await _commonService.GetOrderBy(baseParam);
            var result = await _materialService.GetListMaterial(baseParam);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUpdateMaterial(MaterialDto material)
        {
            var result = await _materialService.CreateOrUpdateMaterial(material);
            return Ok(result > 0);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(long Id)
        {
            var result = await _materialService.Delete(Id);
            return Ok(result > 0);
        }
    }
}
