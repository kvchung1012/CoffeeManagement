using Coffee.Application.Category.Dtos;
using Coffee.Application.Common.Dtos;
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
    public class CategoryService : ICategoryService
    {
        private readonly IDbManager _db;
        private readonly IHttpContextAccessor _httpContext;
        public CategoryService(IDbManager db, IHttpContextAccessor httpContext)
        {
            _db = db;
            _httpContext = httpContext;
        }
        public async Task<ListResult<CategoryDto>> GetListCategory(BaseParamModel baseParam)
        {
            var par = new DynamicParameters();
            par.AddBaseParam(baseParam);
            var res = await _db.QueryAsync<CategoryDto>("Sp_Get_GetListCategory", par);
            var totalCount = par.Get<int>("@TotalCount");
            return new ListResult<CategoryDto>( res, totalCount);
        }

        public async Task<int> CreateOrUpdateCategory(CategoryDto category)
        {
            var par = new DynamicParameters();
            par.Add("@Id", category.Id);
            par.Add("@Code", category.Code);
            par.Add("@Name", category.Name);
            par.Add("@CreatedBy", ((IdentityModel)_httpContext.HttpContext.User.Identity).Id);
            par.Add("@UpdatedBy", ((IdentityModel)_httpContext.HttpContext.User.Identity).Id);
            par.Add("@Status", category.Status);
            var result = await _db.ExecuteAsync("Sp_CreateUpdate_Category", par);
            return result;
        }

        public async Task<int> Delete(long Id)
        {
            var par = new DynamicParameters();
            par.Add("@Id", Id);
            var result = await _db.ExecuteAsync("Sp_Del_Category", par);
            return result;
        }


        public async Task<List<SelectBoxDataDto>> GetAll()
        {
            return await _db.QueryAsync<SelectBoxDataDto>("select Id,Name from Categories where IsDeleted = 0 and Status = 2", null, null, System.Data.CommandType.Text);
        }
    }
}
