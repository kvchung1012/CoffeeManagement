using Coffee.Application.Category.Dtos;
using Coffee.Core.BaseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coffee.Application
{
    public interface ICategoryService
    {
       public Task<ListResult<CategoryDto>> GetListCategory(BaseParamModel baseParam);
       public Task<int> CreateOrUpdateCategory(CategoryDto category);
       public Task<int> Delete(long Id);
    }
}
