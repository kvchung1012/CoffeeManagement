using Coffee.Application;
using Coffee.Application.Mail.Dto;
using Coffee.Application.Order.Dto;
using Coffee.Application.PaymentGateway;
using Coffee.WebApi.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Coffee.Core.Constant.Constant;

namespace Coffee.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IPaymentGatewayService _paymentGatewayService;
        private readonly ISaleCodeService _saleCodeService;
        private readonly IMailService _mailService;
        public HomeController(ICategoryService categoryService
                            , IProductService productService
                            , IOrderService orderService
                            , IPaymentGatewayService paymentGatewayService
                            , ISaleCodeService saleCodeService
                            , IMailService mailService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _orderService = orderService;
            _paymentGatewayService = paymentGatewayService;
            _saleCodeService = saleCodeService;
            _mailService = mailService;
        }

        [HttpGet("danh-sach-danh-muc")]
        public async Task<IActionResult> GetListCategory()
        {
            var result = await _categoryService.GetAll();
            return Ok(result);
        }

        [HttpGet("danh-sach-san-pham")]
        public async Task<IActionResult> GetListProduct([FromQuery] long categoryId, [FromQuery] long fetch, [FromQuery] long offset)
        {
            var res = await _productService.GetProductClient(categoryId, fetch, offset);
            return Ok(res);
        }

        [HttpGet("danh-sach-san-pham-noi-bat")]
        public async Task<IActionResult> GetListTopProduct()
        {
            var res = await _productService.GetTopProductClient();
            return Ok(res);
        }

        [HttpPost("tao-gio-hang")]
        public async Task<IActionResult> CreateCart(CreateCart cart)
        {
            var res = await _orderService.CreateCart(cart, CartStatus.Draft);
            if (res > 0)
            {
                var vnpurl = await _paymentGatewayService.GetPaymentUrl(res);
                return Ok(new CartResult()
                {
                    OrderId = res,
                    PaymentUrl = vnpurl
                });
            }
            return Ok(new CartResult()
            {
                OrderId = -1,
                PaymentUrl = ""
            });

        }
        [CustomAuthorize]
        [HttpGet("kiem-tra-ma-giam-gia/{code}")]
        public async Task<IActionResult> CheckSaleCode(string code)
        {
            var result = await _saleCodeService.CheckSaleCode(code);
            return Ok(result);
        }

        [HttpPost("demo-gui-mail")]
        public async Task<IActionResult> SendMail(MailRequest mailRequest)
        {
            await _mailService.SendEmailAsync(mailRequest);
            return Ok();
        }
    }
}
