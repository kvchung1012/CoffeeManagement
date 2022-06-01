using Coffee.Application;
using Coffee.Application.Permission.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coffee.WebApi.Controllers
{
    public class PermissionController : BaseController
    {
        public ILogger<PermissionController> _logger;
        private readonly IPermissionService _permissionService;
        private readonly ICommonService _commonService;
        public PermissionController(ILogger<PermissionController> logger
                                , IPermissionService permissionService
                                , ICommonService commonService)
        {
            _logger = logger;
            _permissionService = permissionService;
            _commonService = commonService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRole()
        {
            var result = await _permissionService.GetAllRole();
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetPermissionByRole(long Id)
        {
            var result = await _permissionService.GetPermissionByRole(Id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePermissionByRole(List<CreatePermissionRole> permissionRoles)
        {
            var result = await _permissionService.CreatePermissionRole(permissionRoles);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleDto roleDto)
        {
            var result = await _permissionService.CreateRole(roleDto);
            return Ok(result);
        }
    }
}
