using Coffee.Application.Auth;
using Coffee.Core.DbManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            //services.AddSingleton<ICheckPermission, CheckPermission>();
            // dependency injection
            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IDiscountService, DiscountService>();
            services.AddTransient<IMaterialService, MaterialService>();
            services.AddTransient<ISupplierService, SupplierService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<IPositionService, PositionService>();
            services.AddTransient<IImportInvoiceService, ImportInvoiceService>();
            services.AddTransient<IWareHouseService, WareHouseService>();
            services.AddTransient<ISaleCodeService, SaleCodeService>();
            services.AddTransient<IPaymentGatewayService, PaymentGatewayService>();
            services.AddTransient<IMailService, MailService>();
            return services;
        }
    }
}
