using Microsoft.AspNetCore.Mvc;

namespace Coffee.WebApi.Controllers
{
    public class DiscountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
