using Coffee.Application.Category.Dtos;
using Coffee.Application.Common.Dtos;
using Coffee.Core.BaseModel;
using Coffee.Core.DbManager;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly IDbManager _db;
        public CategoryService(IDbManager db)
        {
            _db = db;
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
            par.Add("@CreatedBy", 1);
            par.Add("@UpdatedBy", 1);
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
    }
}
