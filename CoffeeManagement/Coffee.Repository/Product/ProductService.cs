using Coffee.Application.Common.Dtos;
using Coffee.Application.Product.Dto;
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
    public class ProductService : IProductService
    {
        private readonly IDbManager _db;
        private readonly IHttpContextAccessor _httpContext;
        public ProductService(IDbManager db, IHttpContextAccessor httpContext)
        {
            _db = db;
            _httpContext = httpContext;
        }
        public async Task<long> CreateOrUpdateProduct(ProductCreateDto product)
        {
            var con = _db.GetConnection;
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            using (var transaction = con.BeginTransaction())
            {
                try
                {
                    var param = new DynamicParameters();
                    param.AddOutputId(product.Id);
                    param.Add("@Code", product.Code);
                    param.Add("@Name", product.Name);
                    param.Add("@Image", product.Image);
                    param.Add("@Description", product.Description);
                    param.Add("@CategoryId", product.CategoryId);
                    param.Add("@IsCombo", product.IsCombo);
                    param.Add("@IsTop", product.IsTop);
                    param.Add("@Status", product.Status);
                    var result = await _db.ExecuteAsync("Sp_CreateUpdate_Product", param,transaction);
                    if (product.Id == 0)
                        product.Id = param.GetOutputId();

                    // xóa các trường dữ liệu cũ

                    var delPar = new DynamicParameters();
                    delPar.Add("@ProductId", product.Id);
                    result = await _db.ExecuteAsync("Sp_Del_ProductRef", delPar, transaction);

                    // thêm combo
                    if (product.IsCombo)
                    {
                        foreach (var item in product.ProductCombo)
                        {
                            var par = new DynamicParameters();
                            par.Add("@ProductId", product.Id);
                            par.Add("@@ProductRefId", item.ProductRefId);
                            result = await _db.ExecuteAsync("Sp_Create_ProductCombo", par,transaction);
                        }
                    }
                    foreach (var item in product.ProductPrice)
                    {
                        var par = new DynamicParameters();
                        par.Add("@ProductId", product.Id);
                        par.Add("@Price", item.Price);
                        par.Add("@StartTime", item.StartTime);
                        par.Add("@EndTime", item.EndTime);
                        result = await _db.ExecuteAsync("Sp_Create_ProductPrice", par,transaction);
                    }
                    transaction.Commit();
                    return product.Id;
                }
                catch
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

        public Task<int> Delete(long Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SelectBoxDataDto>> GetListCombo()
        {
            return await _db.QueryAsync<SelectBoxDataDto>("select Id,Name from Products where IsDeleted = 0 and isCombo = 0", null, null, System.Data.CommandType.Text);
        }

        public async Task<List<SelectBoxDataDto>> GetListSelectBox(int Id)
        {
            return await _db.QueryAsync<SelectBoxDataDto>($"select Id,Name from Products where IsDeleted = 0 and ({Id} = 0 or CategoryId = {Id})", null, null, System.Data.CommandType.Text);
        }

        public async Task<ListResult<ProductDto>> GetListProduct(BaseParamModel baseParam)
        {
            var par = new DynamicParameters();
            par.AddBaseParam(baseParam);
            var res = await _db.QueryAsync<ProductDto>("Sp_Get_GetListProduct", par);
            var totalCount = par.Get<int>("@TotalCount");
            return new ListResult<ProductDto>(res, totalCount);
        }

        public async Task<ProductCreateDto> GetProductById(long id)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            var product = await _db.QuerySingleAsync<ProductCreateDto>("Sp_Get_GetProductById", par);
            if (product == null)
                throw new Exception("Không tồn tại");

            product.ProductPrice = await _db.QueryAsync<ProductPriceDto>("Sp_Get_GetProductPriceById", par);
            product.ProductCombo = await _db.QueryAsync<ProductComboDto>("Sp_Get_GetProductComboById", par);
            return product;
        }

        public async Task<List<ProductDto>> GetTopProduct()
        {
            var par = new DynamicParameters();
            var res = await _db.QueryAsync<ProductDto>("Sp_Get_GetTopProduct", par);
            return res;
        }
    }
}