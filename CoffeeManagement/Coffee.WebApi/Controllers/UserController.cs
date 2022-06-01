using Coffee.Application;
using Coffee.Application.Users.Dtos;
using Coffee.Core.BaseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coffee.WebApi.Controllers
{
    public class UserController : BaseController
    {
        public ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly ICommonService _commonService;
        public UserController(ILogger<UserController> logger
                                , IUserService userService
                                , ICommonService commonService)
        {
            _logger = logger;
            _userService = userService;
            _commonService = commonService;
        }

        [HttpPost]
        public async Task<IActionResult> GetListUser(BaseParamModel baseParam)
        {
            baseParam.FilterString = await _commonService.GetFilterString(baseParam);
            baseParam.OrderBy = await _commonService.GetOrderBy(baseParam);
            var result = await _userService.GetListUser(baseParam);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetListRoleByUser(long Id)
        {
            var result = await _userService.GetListRoleByUser(Id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserRole(List<CreateUserRole> input)
        {
            var result = await _userService.CreateUserRole(input);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStaff()
        {
            var result = await _userService.GetAllStaff();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto input)
        { 
            var result = await _userService.CreateUser(input);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUserById(long Id)
        {
            var result = await _userService.GetUserById(Id);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetPositionByUserId(long Id)
        {
            var result = await _userService.GetPositionByUserId(Id);
            return Ok(result);
        }

    }
}
