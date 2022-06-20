using Coffee.Application.WareHouse.Dto;
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
    public class WareHouseService : IWareHouseService
    {
        private readonly IDbManager _db;
        private readonly IHttpContextAccessor _httpContext;
        public WareHouseService(IDbManager db, IHttpContextAccessor httpContext)
        {
            _db = db;
            _httpContext = httpContext;
        }

        public async Task<long> CreateExportInvoice(CreateExportInvoiceDto createExportInvoiceDto)
        {
            var con = _db.GetConnection;
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            using (var transaction = con.BeginTransaction())
            {
                try
                {
                    var par = new DynamicParameters();
                    par.AddOutputId(createExportInvoiceDto.Id);
                    par.Add("@Code", $"COFFEX{DateTime.Now.Ticks}");
                    par.Add("@Description", createExportInvoiceDto.Description);
                    par.Add("@Note", createExportInvoiceDto.Note);
                    par.Add("@ExportTo", createExportInvoiceDto.ExportTo);
                    par.AddCreatedByDefault(_httpContext);
                    var result = await _db.ExecuteAsync("Sp_Creare_ExportInvoice", par, transaction);
                    createExportInvoiceDto.Id = par.GetOutputId();

                    foreach (var item in createExportInvoiceDto.ExportInvoiceDetails)
                    {
                        long resultSub = 1;
                        var param = new DynamicParameters();
                        param.Add("@ExportInvoiceId", createExportInvoiceDto.Id);
                        param.Add("@WarehouseId", item.WarehouseId);
                        param.Add("@Stock", item.Stock);
                        param.Add("@Result", resultSub, System.Data.DbType.Int64, System.Data.ParameterDirection.InputOutput);
                        var _ = await _db.ExecuteAsync("Sp_Create_ExportInvoiceDetail", param, transaction);
                        resultSub = param.Get<long>("@Result");
                        // false
                        if (resultSub < 0)
                        {
                            transaction.Rollback();
                            return -1;
                        }
                    }
                    transaction.Commit();
                    return createExportInvoiceDto.Id;
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

        public async Task<ListResult<ExportInvoiceDto>> GetListExportInvoice(BaseParamModel baseParam)
        {
            var par = new DynamicParameters();
            par.AddBaseParam(baseParam);
            var res = await _db.QueryAsync<ExportInvoiceDto>("Sp_Get_GetListExportInvoice", par);
            var totalCount = par.Get<int>("@TotalCount");
            return new ListResult<ExportInvoiceDto>(res, totalCount);
        }

        public async Task<ListResult<WareHouseDto>> GetListWareHouse(GetWareHouse input)
        {
            long count = 0;
            var par = new DynamicParameters();
            par.Add("@MaterialId", input.MaterialId);
            par.Add("@Skip", input.Skip);
            par.Add("@Take", input.Take);
            par.Add("TotalCount", count);
            var result = await _db.QueryAsync<WareHouseDto>("Sp_Get_GetWareHouse", par);
            return new ListResult<WareHouseDto>(result, par.Get<long>("@TotalCount"));
        }

        public async Task<List<MaterialDetailWareHouse>> GetListWareHouseDetail(long Id)
        {
            long count = 0;
            var par = new DynamicParameters();
            par.Add("@Id", Id);
            var result = await _db.QueryAsync<MaterialDetailWareHouse>("Sp_Get_GetWareHouseDetail", par);
            return result;
        }
    }
}
