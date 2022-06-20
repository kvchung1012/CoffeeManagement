using Coffee.Application.PaymentGateway;
using Coffee.EntityFramworkCore;
using Coffee.EntityFramworkCore.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Coffee.Application;
using static Coffee.Core.Constant.Constant;
using Coffee.Application.Mail.Dto;
using System.Collections.Generic;

namespace Coffee.WebApi.Controllers
{
    [ApiController]
    [Route("Home")]
    public class PaymentGatewayController : ControllerBase
    {
        private IConfiguration _configuration;
        private CoffeeDbContext _coffeeDbContext;
        private IMailService _mailService;
        private IOrderService _orderService;
        public PaymentGatewayController(IConfiguration configuration
                                        , CoffeeDbContext coffeeDbContext
                                        , IMailService mailService
                                        , IOrderService orderService)
        {
            _configuration = configuration;
            _coffeeDbContext = coffeeDbContext;
            _mailService = mailService;
            _orderService = orderService;
        }
        [HttpGet("ket-qua-thanh-toan")]
        public async Task<IActionResult> PaymentResult()
        {
            if (HttpContext.Request.Query.Count > 0)
            {
                string vnp_HashSecret = _configuration.GetSection("Vnpay:vnp_HashSecret").Value; //Chuoi bi mat
                var vnpayData = HttpContext.Request.Query;
                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (string s in vnpayData.Keys)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }
                //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
                //vnp_TransactionNo: Ma GD tai he thong VNPAY
                //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
                //vnp_SecureHash: HmacSHA512 cua du lieu tra ve

                string orderId = vnpay.GetResponseData("vnp_TxnRef");
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                string vnp_SecureHash = vnpay.GetResponseData("vnp_SecureHash");
                String TerminalID = vnpay.GetResponseData("vnp_TmnCode");
                long amount = Int64.Parse(vnpay.GetResponseData("vnp_Amount")) / 100;
                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        // thành công thực hiện action tại đây
                        long Id = Int64.Parse(orderId.Split("_")[0]);
                        var order = _coffeeDbContext.Orders.Find(Id);
                        order.Status = CartStatus.Success;
                        order.ReceiveMoney = amount;
                        order.ChangeMoney = 0;
                        _coffeeDbContext.SaveChanges();

                        string listproduct = "";
                        var orderdetail = await _orderService.GetListOrderDetail(order.Id);
                        foreach (var item in orderdetail)
                        {
                            var price = item.PriceSale > 0 ? item.PriceSale : item.Price;
                            listproduct += $"<tr><td>{item.Name}</td> <td> {item.Stock} </td > <td> {price} </td><td style = \"text-align: right;\"> {price * item.Stock} </td></tr> ";
                        }
                        // send mail in here
                        MailRequest mailRequest = new MailRequest()
                        {
                            Subject = "Hóa đơn thanh toán Coffee Amazing",
                            TemplateMail = "cart",
                            ToEmail = new List<string>() { _coffeeDbContext.Users.Find(order.UserId).Email },
                        };
                        mailRequest.ShortCode.Add("##CODE##", order.Code);
                        mailRequest.ShortCode.Add("##CREATE_TIME##", order.CreatedTime.Value.ToString("dd/MM/yyyy HH:mm:ss"));
                        mailRequest.ShortCode.Add("##EMPLOYEE_NAME##", "");
                        mailRequest.ShortCode.Add("##ORDER_DETAIL##", listproduct);
                        mailRequest.ShortCode.Add("##TOTAL_PRICE##", order.TotalPrice.ToString());
                        await _mailService.SendEmailAsync(mailRequest);
                    }
                }
            }
            return Ok();
        }
    }
}
