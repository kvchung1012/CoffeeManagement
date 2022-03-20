using Coffee.Application.Auth;
using Coffee.Application.Common;
using Coffee.Application.Users;
using Coffee.Core.DbManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IDbManager>(provider =>
            {
                var Configuration = provider.GetService<IConfiguration>();
                return new DbManager(Configuration);
            });
            // dependency injection
            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            return services;
        }
    }
}
