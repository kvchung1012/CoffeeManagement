using Coffee.Core.Auth;
using Coffee.Core.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Coffee.WebApi.Auth
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute(string role) : base(typeof(CustomAuthorizeActionFilter))
        {
            Arguments = new object[] { role };
        }
    }


    public class CustomAuthorizeActionFilter : IAuthorizationFilter
    {
        private readonly string _role;

        public CustomAuthorizeActionFilter(string role)
        {
            _role = role;
        }

        /// <summary>
        /// Xử lý khi người dùng check auth
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                //Lấy token từ header
                var token = context.HttpContext.Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(token)) token = "Bearer " + context.HttpContext.Request.Cookies["Authorization"];
                if (string.IsNullOrEmpty(token))
                {
                    context.HttpContext.Response.ContentType = "application/json";
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Result = new JsonResult(new ExceptionResultModel
                    {
                        Message = new[] { "Chưa đăng nhập" }
                    });
                    return;
                }
                if (!token.StartsWith("Bearer"))
                {
                    context.HttpContext.Response.ContentType = "application/json";
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Result = new JsonResult(new ExceptionResultModel
                    {
                        Message = new[] { "Chưa đăng nhập" }
                    });
                }
                string signature = token.Split(".").Last();

                //var _cacheManager = (ICacheManager)context.HttpContext.RequestServices.GetService(typeof(ICacheManager));
                //var isValidSignature = Task.Run(async () => await _cacheManager.GetAsync<bool>(AuthHelper.GetNameCacheToken(signature))).Result;
                //// Nếu chữ ký hết hạn sẽ trả về chưa được Auth
                //if (!isValidSignature)
                //{
                //    context.HttpContext.Response.ContentType = "application/json";
                //    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                //    context.Result = new JsonResult(new ExceptionResultModel
                //    {
                //        Message = new[] { "Chưa đăng nhập" }
                //    });
                //    return;
                //}
                //Lấy thông tin từ token
                var tokenPayload = JwtHelper.ValidateToken(token);
                // Lấy thông tin người dùng gán vào http context
                var UserInfo = new IdentityModel()
                {
                    Id = tokenPayload.Id,
                    FullName = tokenPayload.FullName,
                    Username = tokenPayload.UserName,
                    Email = tokenPayload.Email,
                    Phone = tokenPayload.Phone,
                    IsAuthenticated = true,
                    AuthenticationType = "Bearer",
                    Signature = signature
                };
                context.HttpContext.User = new System.Security.Principal.GenericPrincipal(UserInfo, null);

                //Kiểm tra quyền người dùng yêu cầu
                if (!string.IsNullOrEmpty(_role))
                {
                    var _checkPermission = (ICheckPermission)context.HttpContext.RequestServices.GetService(typeof(ICheckPermission));
                    var allow = Task.Run(
                        async () => await _checkPermission.CheckPermissionAsync(UserInfo.Id,_role)
                        ).Result;
                    if (allow != true)
                    {
                        context.HttpContext.Response.ContentType = "application/json";
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        context.Result = new JsonResult(new ExceptionResultModel
                        {
                            Message = new[] { "Không có quyền truy cập" }
                        });
                        return;
                    }
                }
            }
            catch (Exception)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = new JsonResult(new ExceptionResultModel
                {
                    Message = new[] { "Không có quyền truy cập" }
                });
            }
        }
    }
}
