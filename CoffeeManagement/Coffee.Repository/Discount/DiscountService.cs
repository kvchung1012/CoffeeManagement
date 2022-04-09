using Coffee.Application.Discount.Dto;
using Coffee.Core.BaseModel;
using Coffee.Core.DbManager;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application
{
    public class DiscountService : IDiscountService
    {
        private readonly IDbManager _db;
        public DiscountService(IDbManager db)
        {
            _db = db;
        }
        public async Task<int> CreateOrUpdateDiscount(DiscountDto discount)
        {
            var par = new DynamicParameters();
            par.Add("@Id", discount.Id);
            par.Add("@SaleType", discount.SaleType);
            par.Add("@Value", discount.Value);
            par.Add("@StartTime", discount.StartTime);
            par.Add("@EndTime", discount.EndTime);
            par.Add("@CreatedBy", 1);
            par.Add("@UpdatedBy", 1);
            par.Add("@Status", discount.Status);
            var result = await _db.ExecuteAsync("Sp_CreateUpdate_Discount", par);
            return result;
        }

        public async Task<int> CreateProductDiscount(ProductDiscountDto productDiscount)
        {
            var par = new DynamicParameters();
            par.Add("@ProductId", productDiscount.ProductId);
            par.Add("@DiscountId", productDiscount.DiscountId);
            var result = await _db.ExecuteAsync("Sp_Create_ProductDiscount", par);
            return result;
        }

        public async Task<int> Delete(long Id)
        {
            var par = new DynamicParameters();
            par.Add("@Id", Id);
            var result = await _db.ExecuteAsync("Sp_Del_Discount", par);
            return result;
        }

        public async Task<int> DeleteProductDiscount(long Id)
        {
            var par = new DynamicParameters();
            par.Add("@Id", Id);
            var result = await _db.ExecuteAsync("Sp_Del_ProductDiscount", par);
            return result;
        }

        public async Task<ListResult<DiscountDto>> GetListDiscount(BaseParamModel baseParam)
        {
            var par = new DynamicParameters();
            par.AddBaseParam(baseParam);
            var res = await _db.QueryAsync<DiscountDto>("Sp_Get_GetListDiscount", par);
            var totalCount = par.Get<int>("@TotalCount");
            return new ListResult<DiscountDto>(res, totalCount);
        }

        public async Task<List<ProductDiscountDto>> GetListProductDiscount(long productId, long discountId)
        {
            var par = new DynamicParameters();
            par.Add("@ProductId", productId);
            par.Add("@DiscountId", discountId);
            var result = await _db.QueryAsync<ProductDiscountDto>("sp_get_productdiscount", par);
            return result;
        }
    }
}
