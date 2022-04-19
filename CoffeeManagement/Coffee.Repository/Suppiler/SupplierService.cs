using Coffee.Application.Material.Dto;
using Coffee.Application.Suppiler.Dto;
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
    public class SupplierService : ISupplierService
    {
        private readonly IDbManager _db;
        public SupplierService(IDbManager db)
        {
            _db = db;
        }
        public async Task<int> CreateOrUpdateSuppiler(SupplierDto suppiler)
        {
            var par = new DynamicParameters();
            par.Add("@Id", suppiler.Id);
            par.Add("@Code", suppiler.Code);
            par.Add("@Name", suppiler.Name);
            par.Add("@CreatedBy", 1);
            par.Add("@UpdatedBy", 1);
            par.Add("@Status", suppiler.Status);
            var result = await _db.ExecuteAsync("Sp_CreateUpdate_Suppiler", par);
            return result;
        }

        public async Task<int> Delete(long Id)
        {
            var par = new DynamicParameters();
            par.Add("@Id", Id);
            var result = await _db.ExecuteAsync("Sp_del_Suppiler", par);
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
    }
}
