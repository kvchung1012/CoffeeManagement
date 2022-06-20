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
        public async Task<long> CreateOrUpdateDiscount(CreateDiscountDto discount)
        {
            var con = _db.GetConnection;
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            using (var transaction = con.BeginTransaction())
            {
                try
                {
                    var par = new DynamicParameters();
                    par.AddOutputId(discount.Id);
                    par.Add("@Code", discount.Code);
                    par.Add("@Name", discount.Name);
                    par.Add("@SaleType", discount.SaleType);
                    par.Add("@Value", discount.Value);
                    par.Add("@StartTime", discount.StartTime);
                    par.Add("@EndTime", discount.EndTime);
                    par.Add("@CreatedBy", 1);
                    par.Add("@UpdatedBy", 1);
                    par.Add("@Status", discount.Status);
                    var result = await _db.ExecuteAsync("Sp_CreateUpdate_Discount", par, transaction);
                    // thêm mới
                    if (discount.Id == 0)
                        discount.Id = par.GetOutputId();
                    // cập nhật thì xóa những dòng cũ
                    else
                    {
                        var param = new DynamicParameters();
                        param.Add("@Id", discount.Id);
                        var _ = await _db.ExecuteAsync("sp_del_productdiscount", param, transaction);
                    }
                    foreach (var item in discount.ProductDiscount)
                    {
                        var param = new DynamicParameters();
                        param.Add("@ProductId", item);
                        param.Add("@DiscountId", discount.Id);
                        var _ = await _db.ExecuteAsync("Sp_Create_ProductDiscount", param, transaction);
                    }
                    transaction.Commit();
                    return discount.Id;
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

        public async Task<CreateDiscountDto> GetDiscountById(long Id)
        {
            var par = new DynamicParameters();
            par.Add("@Id", Id);
            var result = await _db.QueryFirstOrDefaultAsync<CreateDiscountDto>("Sp_Get_GetDiscountById", par);
            result.ProductDiscount = await _db.QueryAsync<int>("Sp_Get_GetProductIdByDiscountId", par);
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
