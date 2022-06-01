using Coffee.Application.Material.Dto;
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
    public class MaterialService : IMaterialService
    {
        private readonly IDbManager _db;
        private readonly IHttpContextAccessor _httpContext;
        public MaterialService(IDbManager db, IHttpContextAccessor httpContext)
        {
            _db = db;
            _httpContext = httpContext;
        }
        public async Task<int> CreateOrUpdateMaterial(MaterialDto material)
        {
            var par = new DynamicParameters();
            par.Add("@Id", material.Id);
            par.Add("@Code", material.Code);
            par.Add("@Name", material.Name);
            par.Add("@Unit", material.Unit);
            par.Add("@CreatedBy", ((IdentityModel)_httpContext.HttpContext.User.Identity).Id);
            par.Add("@UpdatedBy", ((IdentityModel)_httpContext.HttpContext.User.Identity).Id);
            par.Add("@Status", material.Status);
            var result = await _db.ExecuteAsync("Sp_CreateUpdate_Material", par);
            return result;
        }

        public async Task<int> Delete(long Id)
        {
            var par = new DynamicParameters();
            par.Add("@Id", Id);
            var result = await _db.ExecuteAsync("Sp_del_material", par);
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
