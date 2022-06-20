using Coffee.Application.PaymentGateway;
using Coffee.EntityFramworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application
{
    public class PaymentGatewayService : IPaymentGatewayService
    {
        private IConfiguration _configuration;
        private CoffeeDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContext;
        public PaymentGatewayService(IConfiguration configuration
                                    , CoffeeDbContext coffeeDbContext
                                    , IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _dbContext = coffeeDbContext;
            _httpContext = httpContextAccessor;
        }
        public async Task<string> GetPaymentUrl(long orderId)
        {
            var order = await _dbContext.Orders.FindAsync(orderId);
            var vnp_TmnCode = _configuration.GetSection("Vnpay:vnp_TmnCode").Value;
            var vnp_HashSecret = _configuration.GetSection("Vnpay:vnp_HashSecret").Value;
            var vnp_Url = _configuration.GetSection("Vnpay:vnp_Url").Value;
            var vnp_ReturnUrl = _configuration.GetSection("Vnpay:vnp_ReturnUrl").Value;
            VnPayLibrary vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (Convert.ToInt64(order.TotalPrice)*100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            vnpay.AddRequestData("vnp_BankCode", "");
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(_httpContext));
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + order.Id);
            vnpay.AddRequestData("vnp_OrderType", "topup"); //default value: other
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_ReturnUrl);
            vnpay.AddRequestData("vnp_TxnRef", $"{order.Id}_{DateTime.Now.Ticks}"); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày
            vnpay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddDays(1).ToString("yyyyMMddHHmmss"));
            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            return paymentUrl;
        }
    }
}
