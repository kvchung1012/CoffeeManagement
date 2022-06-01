using Coffee.Application;
using Coffee.Application.Product.Dto;
using Coffee.Core.BaseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Coffee.WebApi.Controllers
{
    [AllowAnonymous]
    public class ProductController : BaseController
    {
        public ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly ICommonService _commonService;
        public ProductController(ILogger<ProductController> logger
                                , IProductService productService
                                , ICommonService commonService)
        {
            _logger = logger;
            _productService = productService;
            _commonService = commonService;
        }

        [HttpPost]
        public async Task<IActionResult> GetListProduct(BaseParamModel baseParam)
        {
            baseParam.FilterString = await _commonService.GetFilterString(baseParam);
            baseParam.OrderBy = await _commonService.GetOrderBy(baseParam);
            var result = await _productService.GetListProduct(baseParam);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUpdateProduct(ProductCreateDto product)
        {
            var result = await _productService.CreateOrUpdateProduct(product);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetListCombo()
        {
            var result = await _productService.GetListCombo();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(long Id)
        {
            var result = await _productService.GetProductById(Id);
            return Ok(result);
        }
    }
}
