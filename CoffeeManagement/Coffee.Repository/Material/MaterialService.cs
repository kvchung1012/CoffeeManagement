using Coffee.Application.Material.Dto;
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
    public class MaterialService : IMaterialService
    {
        private readonly IDbManager _db;
        public MaterialService(IDbManager db)
        {
            _db = db;
        }
        public async Task<int> CreateOrUpdateMaterial(MaterialDto material)
        {
            var par = new DynamicParameters();
            par.Add("@Id", material.Id);
            par.Add("@Code", material.Code);
            par.Add("@Name", material.Name);
            par.Add("@Unit", material.Unit);
            par.Add("@CreatedBy", 1);
            par.Add("@UpdatedBy", 1);
            par.Add("@Status", material.Status);
            var result = await _db.ExecuteAsync("Sp_CreateUpdate_Material", par);
            return result;
        }

        public async Task<int> Delete(long Id)
        {
            var par = new DynamicParameters();
            par.Add("@Id", Id);
            var result = await _db.ExecuteAsync("Sp_del_ProductDiscount", par);
            return result;
        }

        public async Task<ListResult<MaterialDto>> GetListMaterial(BaseParamModel baseParam)
        {
            var par = new DynamicParameters();
            par.AddBaseParam(baseParam);
            var res = await _db.QueryAsync<MaterialDto>("Sp_Get_GetListMaterials", par);
            var totalCount = par.Get<int>("@TotalCount");
            return new ListResult<MaterialDto>(res, totalCount);
        }
    }
}
