using Coffee.Application.ImportInvoice.Dto;
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
    public class ImportInvoiceService : IImportInvoiceService
    {
        private readonly IDbManager _db;
        private readonly IHttpContextAccessor _httpContext;
        public ImportInvoiceService(IDbManager db, IHttpContextAccessor httpContext)
        {
            _db = db;
            _httpContext = httpContext;
        }

        public async Task<long> CreateImportInvoice(CreateImportInvoice createImport)
        {
            var con = _db.GetConnection;
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            using (var transaction = con.BeginTransaction())
            {
                try
                {
                    var code = $"COFFIM{DateTime.Now.Ticks}";
                    var par = new DynamicParameters();
                    par.AddOutputId(createImport.Id);
                    par.Add("@Code", code);
                    par.Add("@Description", createImport.Description);
                    par.Add("@Note", createImport.Note);
                    par.Add("@SupplierId", createImport.SupplierId);
                    par.Add("@TotalCost", createImport.ImportInvoiceDetails.Select(x => x.Price * x.Stock).Sum());
                    par.Add("@Status", createImport.Status);
                    par.AddCreatedByDefault(_httpContext);
                    var result = await _db.ExecuteAsync("Sp_Create_ImportInvoice", par, transaction);
                    createImport.Id = par.GetOutputId();

                    foreach(var item in createImport.ImportInvoiceDetails)
                    {
                        var param = new DynamicParameters();
                        param.AddOutputId(item.Id);
                        param.Add("@ImportInvoiceId", createImport.Id);
                        param.Add("@MaterialId", item.MaterialId);
                        param.Add("@Stock", item.Stock);
                        param.Add("@Price", item.Price);
                        param.Add("@TotalPrice", item.Price*item.Stock);
                        result = await _db.ExecuteAsync("Sp_Create_ImportInvoiceDetail", param, transaction);
                    }

                    foreach (var item in createImport.ImportInvoiceDetails)
                    {
                        var param = new DynamicParameters();
                        param.AddOutputId(item.Id);
                        param.Add("@Code", code);
                        param.Add("@MaterialId", item.MaterialId);
                        param.Add("@Stock", item.Stock);
                        param.Add("@StartTime", item.StartTime);
                        param.Add("@ExpriedTime", item.ExpriedTime);
                        param.AddCreatedByDefault(_httpContext);
                        result = await _db.ExecuteAsync("Sp_Create_WareHouse", param, transaction);
                    }
                    transaction.Commit();
                    return createImport.Id;
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

        public async Task<ListResult<ImportInvoiceDto>> GetListImportInvoice(BaseParamModel baseParam)
        {
            var par = new DynamicParameters();
            par.AddBaseParam(baseParam);
            var res = await _db.QueryAsync<ImportInvoiceDto>("Sp_Get_GetListImportInvoice", par);
            var totalCount = par.Get<int>("@TotalCount");
            return new ListResult<ImportInvoiceDto>(res, totalCount);
        }
    }
}
