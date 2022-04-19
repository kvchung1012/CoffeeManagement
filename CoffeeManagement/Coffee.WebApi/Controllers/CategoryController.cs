using Coffee.Application;
using Coffee.Application.Category.Dtos;
using Coffee.Application;
using Coffee.Core.BaseModel;
using Coffee.WebApi.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Coffee.WebApi.Controllers
{
    [CustomAuthorize]
    public class CategoryController : BaseController
    {
        public ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly ICommonService _commonService;
        public CategoryController(ILogger<CategoryController> logger
                                , ICategoryService categoryService
                                , ICommonService commonService)
        {
            _logger = logger;
            _categoryService = categoryService;
            _commonService = commonService;
        }

        [HttpPost]
        public async Task<IActionResult> GetListCategory(BaseParamModel baseParam)
        {
            baseParam.FilterString = await _commonService.GetFilterString(baseParam);
            baseParam.OrderBy = await _commonService.GetOrderBy(baseParam);
            var result = await _categoryService.GetListCategory(baseParam);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUpdateCategory(CategoryDto category)
        {
            var result = await _categoryService.CreateOrUpdateCategory(category);
            return Ok(result > 0);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(long Id)
        {
            var result = await _categoryService.Delete(Id);
            return Ok(result > 0);
        }
    }
}
