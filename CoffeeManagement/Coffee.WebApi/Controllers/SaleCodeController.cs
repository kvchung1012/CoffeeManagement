using Coffee.Application;
using Coffee.Application.Mail.Dto;
using Coffee.Application.SaleCode.Dto;
using Coffee.Core.BaseModel;
using Coffee.EntityFramworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee.WebApi.Controllers
{
    public class SaleCodeController : BaseController
    {
        public ILogger<DiscountController> _logger;
        private readonly ISaleCodeService _saleCodeService;
        private readonly ICommonService _commonService;
        private readonly CoffeeDbContext _dbContext;
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;
        public SaleCodeController(ILogger<DiscountController> logger
                                , ISaleCodeService saleCodeService
                                , ICommonService commonService
                                , CoffeeDbContext coffeeDbContext
                                , IMailService mailService
                                , IConfiguration configuration)
        {
            _logger = logger;
            _saleCodeService = saleCodeService;
            _commonService = commonService;
            _dbContext = coffeeDbContext;
            _mailService = mailService;
            _configuration = configuration;
        }


        [HttpPost]
        public async Task<IActionResult> GetListSaleCode(BaseParamModel baseParam)
        {
            baseParam.FilterString = await _commonService.GetFilterString(baseParam);
            baseParam.OrderBy = await _commonService.GetOrderBy(baseParam);
            var result = await _saleCodeService.GetListSaleCode(baseParam);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(CreateSaleCodeDto input)
        {
            var result = await _saleCodeService.CreateOrUpdateSaleCode(input);
            if (result > 0)
            {
                MailRequest mailRequest = new MailRequest()
                {
                    Subject = "Tri ân khách hàng tặng mã khuyến mãi",
                    TemplateMail = "salecode",
                    ToEmail = _dbContext.Users.Select(x => x.Email).ToList(),
                };
                var salecode = _dbContext.SaleCodes.Find(result);
                mailRequest.ShortCode.Add("##SALE_VALUE##", salecode.SaleType ? salecode.Value.ToString() : salecode.MaxPriceSale.ToString());
                mailRequest.ShortCode.Add("##SALE_CODE##", salecode.Code);
                mailRequest.ShortCode.Add("##LINK_HOME##", _configuration.GetSection("DomainWeb:Cient").Value);
                await _mailService.SendEmailAsync(mailRequest);
            }
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(long Id)
        {
            var result = await _saleCodeService.GetSaleCodeById(Id);
            return Ok(result);
        }
    }
}
