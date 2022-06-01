using Coffee.Application.Material.Dto;
using Coffee.Application.Suppiler.Dto;
using Coffee.Core.Auth;
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
    public class SupplierService : ISupplierService
    {
        private readonly IDbManager _db;
        private readonly IHttpContextAccessor _httpContext;
        public SupplierService(IDbManager db, IHttpContextAccessor httpContext)
        {
            _db = db;
            _httpContext = httpContext;
        }
        public async Task<int> CreateOrUpdateSuppiler(SupplierDto suppiler)
        {
            var par = new DynamicParameters();
            par.Add("@Id", suppiler.Id);
            par.Add("@Code", suppiler.Code);
            par.Add("@Name", suppiler.Name);
            par.Add("@CreatedBy", ((IdentityModel)_httpContext.HttpContext.User.Identity).Id);
            par.Add("@UpdatedBy", ((IdentityModel)_httpContext.HttpContext.User.Identity).Id);
            par.Add("@Status", suppiler.Status);
            var result = await _db.ExecuteAsync("Sp_CreateUpdate_Supplier", par);
            return result;
        }

        public async Task<int> Delete(long Id)
        {
            var par = new DynamicParameters();
            par.Add("@Id", Id);
            var result = await _db.ExecuteAsync("Sp_del_Supplier", par);
            return result;
        }

        public async Task<ListResult<SupplierDto>> GetListSuppiler(BaseParamModel baseParam)
        {
            var par = new DynamicParameters();
            par.AddBaseParam(baseParam);
            var res = await _db.QueryAsync<SupplierDto>("Sp_Get_GetListSupplier", par);
            var totalCount = par.Get<int>("@TotalCount");
            return new ListResult<SupplierDto>(res, totalCount);
        }

        public async Task<List<SupplierDto>> GetAll()
        {
            var par = new DynamicParameters();
            return await _db.QueryAsync<SupplierDto>("Sp_Get_GetAllSupplier", par);
        }
    }
}
