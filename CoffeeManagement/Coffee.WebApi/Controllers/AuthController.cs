using Coffee.Application.Auth;
using Coffee.Application.Auth.Dtos;
using Coffee.Application.Common;
using Coffee.Application.Users;
using Coffee.Application.Users.Dtos;
using Coffee.Core.Auth;
using Coffee.Core.BaseModel;
using Coffee.Core.Helper;
using Coffee.WebApi.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Coffee.Application;
using Microsoft.AspNetCore.Authorization;

namespace Coffee.WebApi.Controllers
{
    public class AuthController : BaseController
    {
        public ILogger<AuthController> _logger;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly ICommonService _commonService;
        public AuthController(ILogger<AuthController> logger
                              , IAuthService authService
                              , IUserService userService
                              , ICommonService commonService)
        {
            _logger = logger;
            _userService = userService;
            _authService = authService;
            _commonService = commonService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto input)
        {
            var user = await _authService.GetUserByUserName(input);
            var check = false;
            // người dùng không tồn tại
            if (user is null)
                return Unauthorized("Tài khoản hoặc mật khẩu không chính xác");

            try
            {
                var hashed = BcryptHelper.GetPasswordHash(input.Password);
                // kiểm tra xem có đúng mật khẩu không
                var checkPassword = BcryptHelper.VerifiedPassword(input.Password, user.HashedPassword);
                if (!checkPassword) return Unauthorized("Tài khoản hoặc mật khẩu không chính xác");

                // kiểm tra xem có quyền truy cập vào hệ thống không
                check = await _authService.CheckPermission(user.Id, "System.Login");
                //if (!check)
                //    return Unauthorized("Tài khoản không có quyền truy cập hệ thống");
                // kiểm tra xem tài khoản có đc active không
            }
            catch (Exception)
            {
                return Unauthorized("Tài khoản hoặc mật khẩu không chính xác");
            }

            var tokenPayload = new JwtPayloadModel()
            {
                Id = user.Id,
                Email = user.Email,
                Phone = user.Phone,
                UserName = user.UserName,
                FullName = user.FullName
            };
            var token = JwtHelper.GenerateJwtToken(tokenPayload);
            //Lấy chữ ký của token
            //string signature = token.Split(".").Last();
            //Lưu thông tin token này vào db để sau này quản lý việc đăng nhập
            //await _authAppService.InsertTokenAsync(user.Id, signature, true);
            //Lưu token vào cache để kiểm tra nhanh hơn
            //await _cacheManager.SetAsync(AuthHelper.GetNameCacheToken(signature), true, AuthHelper.TimeLife);
            //HttpContext.Response.Cookies.Append(AuthHelper.AuthorizationName, $"{token}");
            return Ok(new JwtResultModel(token, check));
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword()
        {
            return Ok();
        }

        [HttpGet("{Token}")]
        [CustomAuthorize]
        public async Task<IActionResult> InitApp(string token)
        {
            // sau kiểm tra và đăng ký những thông tin cần thiết tại đây
            return Ok(true);
        }
    }
}
