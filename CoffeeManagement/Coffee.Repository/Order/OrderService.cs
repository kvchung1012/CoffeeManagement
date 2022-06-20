using Coffee.Application.Order.Dto;
using Coffee.Application.Product.Dto;
using Coffee.Application.SaleCode.Dto;
using Coffee.Core;
using Coffee.Core.BaseModel;
using Coffee.Core.DbManager;
using Coffee.EntityFramworkCore;
using Dapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application
{
    public class OrderService : IOrderService
    {
        private readonly IDbManager _db;
        private readonly ISaleCodeService _saleCodeService;
        private readonly CoffeeDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContext;
        public OrderService(IDbManager db, IHttpContextAccessor httpContext, CoffeeDbContext dbContext, ISaleCodeService saleCodeService)
        {
            _db = db;
            _httpContext = httpContext;
            _dbContext = dbContext;
            _saleCodeService = saleCodeService;
        }

        public async Task<long> CreateCart(CreateCart input, long status)
        {
            var con = _db.GetConnection;
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            using (var transaction = con.BeginTransaction())
            {
                try
                {
                    long userId = 0;
                    // kiểm tra user
                    if (!string.IsNullOrEmpty(input.CustomerPhone) && !string.IsNullOrEmpty(input.CustomerName))
                    {
                        var paramUser = new DynamicParameters();
                        paramUser.AddOutputId(userId);
                        paramUser.Add("@Name", input.CustomerName);
                        paramUser.Add("@Phone", input.CustomerPhone);
                        paramUser.Add("@Email", input.CustomerEmail);
                        await _db.ExecuteAsync("Sp_Create_Customer", paramUser, transaction);
                        userId = paramUser.GetOutputId();
                    }

                    var totalPrice = await CaculatePrice(input, transaction);
                    long saleCodeId = 0;
                    // kiểm tra khuyến mãi cho user
                    if (!String.IsNullOrEmpty(input.SaleCode))
                    {
                        var sale = await GetSaleCode(input.SaleCode, userId, transaction);
                        totalPrice = sale.SaleType ? totalPrice - sale.Value : (totalPrice * ((100 - sale.Value) / 100));
                        saleCodeId = sale.Id;
                    }
                    // insert order
                    long orderId = 0;
                    var paramOrder = new DynamicParameters();
                    paramOrder.AddOutputId(orderId);
                    paramOrder.Add(@"Code", $"COFF${DateTime.Now.Ticks}");
                    paramOrder.Add("@UserId", userId);
                    paramOrder.Add("@TotalPrice", totalPrice);  // auto tính tổng hóa đơn
                    paramOrder.Add("@ReceiveMoney", input.ReceiveMoney);
                    paramOrder.Add("@ChangeMoney", input.ChangeMoney);
                    paramOrder.Add("@Status", status);
                    paramOrder.Add("@SaleCodeId", saleCodeId);
                    paramOrder.AddCreatedByDefault(_httpContext);
                    var res = await _db.ExecuteAsync("Sp_Create_Order", paramOrder, transaction);
                    orderId = paramOrder.GetOutputId();

                    // insert detail
                    foreach (var item in input.Products)
                    {
                        var param = new DynamicParameters();
                        param.Add("@OrderId", orderId);
                        param.Add("@ProductId", item.ProductId);
                        param.Add("@Stock", item.Stock);
                        await _db.ExecuteAsync("Sp_Create_OrderDetail", param, transaction);
                    }

                    transaction.Commit();
                    return orderId;
                }
                catch (UserFriendlyException ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return -1;
                }
                finally
                {
                    con.Close();
                }
            }

        }

        public async Task<ListResult<OrderDto>> GetListOrder(BaseParamModel baseParam)
        {
            var par = new DynamicParameters();
            par.AddBaseParam(baseParam);
            var res = await _db.QueryAsync<OrderDto>("Sp_Get_GetListOrder", par);
            var totalCount = par.Get<int>("@TotalCount");
            return new ListResult<OrderDto>(res, totalCount);
        }

        public async Task<List<OrderDetailDto>> GetListOrderDetail(long Id)
        {
            var par = new DynamicParameters();
            par.Add("@OrderId", Id);
            var res = await _db.QueryAsync<OrderDetailDto>("Sp_Get_OrderDetail", par);
            return res;
        }

        public async Task<OrderFullDto> GetListOrderById(long Id)
        {
            var par = new DynamicParameters();
            par.Add("@Id", Id);
            var res = await _db.QuerySingleAsync<OrderFullDto>("Sp_get_OrderById", par);
            return res;
        }



        // hàm con
        private async Task<decimal> CaculatePrice(CreateCart createCart, IDbTransaction dbTransaction)
        {
            var par = new DynamicParameters();
            par.Add("@ListId", string.Join(",", createCart.Products.Select(x => x.ProductId).ToList()));
            var result = await _db.QueryAsync<ProductDto>("Sp_Get_GetProductByListId", par, dbTransaction);
            decimal totalPrice = 0;
            foreach (var product in result)
            {
                var stock = createCart.Products.FirstOrDefault(x => x.ProductId == product.Id).Stock;
                // không giảm giá
                if (product.Value == 0)
                {
                    totalPrice += product.Price * stock;
                }
                else
                {
                    // tiền mặt
                    if (product.SaleType)
                        totalPrice += (product.Price - product.Value) * stock;
                    else
                        totalPrice += (product.Price * ((100 - product.Value) / 100)) * stock;
                }
            }
            return totalPrice;
        }

        private async Task<SaleCodeDto> GetSaleCode(string saleCode, long userId, IDbTransaction dbTransaction)
        {
            var par = new DynamicParameters();
            par.Add("@Code", saleCode);
            var sale = await _db.QueryFirstOrDefaultAsync<SaleCodeDto>("Sp_Get_GetSaleCodeByCode", par, dbTransaction);
            if (sale == null)
                throw new UserFriendlyException("Mã khuyến mãi không hợp lệ");
            // tính xem có đến lượt mình dùng không
            var countOrder = _dbContext.Orders.Where(x => x.SaleCodeId == sale.Id).Count();
            if (countOrder >= sale.Stock)
                throw new UserFriendlyException("Mã khuyến mãi đã hết lượt dùng");

            var countOrderUser = _dbContext.Orders.Where(x => x.SaleCodeId == sale.Id && x.UserId == userId).Count();
            if (countOrder >= sale.StockByUser)
                throw new UserFriendlyException("Bạn đã dùn hết mã khuyến mãi");
            return sale;
        }
    }
}
