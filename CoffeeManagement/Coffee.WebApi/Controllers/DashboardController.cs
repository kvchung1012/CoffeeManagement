using Coffee.Application;
using Coffee.Application.Dashboard.Dto;
using Coffee.EntityFramworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee.WebApi.Controllers
{
    public class DashboardController : BaseController
    {
        public ILogger<DashboardController> _logger;
        private readonly CoffeeDbContext _dbContext;
        private readonly IProductService _productService;
        public DashboardController(CoffeeDbContext db
                                , ILogger<DashboardController> logger
                                , IProductService productService)
        {
            _dbContext = db;
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTotalSystem()
        {
            // tổng người dùng
            var totalUser = _dbContext.Users.Where(x => x.IsDeleted == false).Count();
            // tổng sản phẩm golive
            var totalProduct = _dbContext.Products.Where(x => x.IsDeleted == false).Count();
            // tổng hóa đơn
            var totalOrder = _dbContext.Orders.Count();
            // tổng hóa đơn đã bán trong ngày
            var totalOrderToday = _dbContext.Orders.Where(x => x.CreatedTime == DateTime.Now).Count();
            // tổng đối tác
            var totalSupplier = _dbContext.Suppliers.Where(x => x.IsDeleted == false).Count();
            return Ok(new TotalSystem()
            {
                Users = totalUser,
                Products = totalProduct,
                Orders = totalOrder,
                OrderToday = totalOrderToday,
                Supplier = totalSupplier
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetTopProduct()
        {
            // danh sách sản phẩm
            var res = await _productService.GetTopProduct();
            return Ok(res);
        }

        [HttpGet("{year}")]
        public async Task<IActionResult> GetRevueneByYear(int year)
        {
            year = year == null ? DateTime.Now.Year : year;
            var revueneInYear = _dbContext.Orders.Where(x => x.CreatedTime.Value.Year == year);
            var revueneMonth = revueneInYear.GroupBy(p => p.CreatedTime.Value.Month, p => p.TotalPrice, (key, g) =>
              new
              {
                  month = key,
                  money = g.Sum()
              });
            decimal[] arr = new decimal[12];
            for (int i = 0; i < 12; i++)
            {
                if (revueneMonth.Any(x => x.month == i + 1))
                {
                    arr[i] = revueneMonth.FirstOrDefault(x => x.month == i + 1).money;
                }
            }
            return Ok(arr);
        }

        [HttpGet("{year}/{month}")]
        public async Task<IActionResult> GetRevueneByMonth(int year, int month)
        {
            year = year == null || year == 0 ? DateTime.Now.Year : year;
            month = month == null || month == 0 ? DateTime.Now.Month : month;
            var revueneInYear = _dbContext.Orders.Where(x => x.CreatedTime.Value.Year == year && x.CreatedTime.Value.Month == month);
            var revueneMonth = revueneInYear.GroupBy(p => p.CreatedTime.Value.Day, p => p.TotalPrice, (key, g) =>
              new
              {
                  day = key,
                  money = g.Sum()
              });
            var countDay = DateTime.DaysInMonth(year, month);
            decimal[] arr = new decimal[countDay + 1];
            for (int i = 0; i < countDay; i++)
            {
                if (revueneMonth.Any(x => x.day == i + 1))
                {
                    arr[i] = revueneMonth.FirstOrDefault(x => x.day == i + 1).money;
                }
            }
            return Ok(arr);
        }
    }
}
