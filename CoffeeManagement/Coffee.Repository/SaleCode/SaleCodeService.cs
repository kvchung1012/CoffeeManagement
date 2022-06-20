using Coffee.Application.SaleCode.Dto;
using Coffee.Core.BaseModel;
using Coffee.Core.DbManager;
using Coffee.EntityFramworkCore;
using Dapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application
{
    public class SaleCodeService : ISaleCodeService
    {
        private readonly IDbManager _db;
        private CoffeeDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContext;
        public SaleCodeService(IDbManager db, IHttpContextAccessor httpContext, CoffeeDbContext coffeeDbContext)
        {
            _db = db;
            _httpContext = httpContext;
            _dbContext = coffeeDbContext;
        }

        /// <summary>
        /// Kiểm tra xem mã code còn hợp lệ hay không
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<SaleCodeDto> CheckSaleCode(string code)
        {
            var par = new DynamicParameters();
            par.Add("@Code", code);
            return await _db.QueryFirstOrDefaultAsync<SaleCodeDto>("Sp_Get_GetSaleCodeByCode", par);
        }

        public async Task<long> CreateOrUpdateSaleCode(CreateSaleCodeDto saleCode)
        {
            var par = new DynamicParameters();
            par.AddOutputId(saleCode.Id);
            par.Add("@Code", saleCode.Code);
            par.Add("@Name", saleCode.Name);
            par.Add("@SaleType", saleCode.SaleType);
            par.Add("@Value", saleCode.Value);
            par.Add("@StartTime", saleCode.StartTime);
            par.Add("@EndTime", saleCode.EndTime);
            par.Add("@Stock", saleCode.Stock);
            par.Add("@StockByUser", saleCode.StockByUser);
            par.Add("@MinPrice", saleCode.MinPrice);
            par.Add("@MaxPriceSale", saleCode.MaxPriceSale);
            par.AddCreatedByDefault(_httpContext);
            par.Add("@Status", saleCode.Status);
            var result = await _db.ExecuteAsync("Sp_Create_SaleCode", par);
            return par.GetOutputId();
        }

        public async Task<ListResult<SaleCodeDto>> GetListSaleCode(BaseParamModel baseParam)
        {
            var par = new DynamicParameters();
            par.AddBaseParam(baseParam);
            var res = await _db.QueryAsync<SaleCodeDto>("Sp_Get_GetListSaleCode", par);
            var totalCount = par.Get<int>("@TotalCount");
            return new ListResult<SaleCodeDto>(res, totalCount);
        }

        public async Task<SaleCodeDto> GetSaleCodeById(long Id)
        {
            var par = new DynamicParameters();
            par.Add("@Id", Id);
            return await _db.QueryFirstOrDefaultAsync<SaleCodeDto>("Sp_Get_GetSaleCodeById", par);
        }

        public async Task<bool> CheckSaleCodeCanUse(long userId, string saleCode)
        {
            var check = _dbContext.SaleCodes.FirstOrDefault(x => x.Code == saleCode && x.StartTime <= DateTime.Now && DateTime.Now <= (x.EndTime ?? DateTime.Now));
            var totalUse = _dbContext.Orders.Where(x => x.SaleCodeId == check.Id).Count();
            var userUse = _dbContext.Orders.Where(x => x.SaleCodeId == check.Id && x.UserId == userId).Count();
            if (check.Stock >= totalUse)
                return false;
            if (check.StockByUser >= userUse)
                return false;
            return true;
        }
    }
}
