using Coffee.Application.Order.Dto;
using Coffee.Core.BaseModel;
using Coffee.Core.DbManager;
using Dapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application
{
    public class OrderService : IOrderService
    {
        private readonly IDbManager _db;
        private readonly IHttpContextAccessor _httpContext;
        public OrderService(IDbManager db, IHttpContextAccessor httpContext)
        {
            _db = db;
            _httpContext = httpContext;
        }

        public async Task<long> CreateCart(CreateCart input)
        {
            var con = _db.GetConnection;
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            using (var transaction = con.BeginTransaction())
            {
                try
                {
                    long userId = 0;
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
                    // kiểm tra user
                    
                    // insert order
                    long orderId = 0;
                    var paramOrder = new DynamicParameters();
                    paramOrder.AddOutputId(orderId);
                    paramOrder.Add(@"Code", $"COFF${DateTime.Now.ToString("ddMMyyHHmmss")}");
                    paramOrder.Add("@UserId", userId);
                    paramOrder.Add("@TotalPrice", input.TotalMoney);
                    paramOrder.Add("@ReceiveMoney", input.ReceiveMoney);
                    paramOrder.Add("@ChangeMoney", input.ChangeMoney);
                    paramOrder.AddCreatedByDefault(_httpContext);
                    var res = await _db.ExecuteAsync("Sp_Create_Order", paramOrder,transaction);
                    orderId = paramOrder.GetOutputId();

                    // insert detail
                    foreach (var item in input.Products)
                    {
                        var param = new DynamicParameters();
                        param.Add("@OrderId", orderId);
                        param.Add("@ProductId", item.ProductId);
                        param.Add("@Stock", item.Stock);
                        await _db.ExecuteAsync("Sp_Create_OrderDetail", param,transaction);
                    }
                    transaction.Commit();
                    return orderId;
                }
                catch
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
            par.Add("@OrderId",Id);
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
    }
}
