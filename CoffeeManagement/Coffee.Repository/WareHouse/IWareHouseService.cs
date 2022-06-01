using Coffee.Application.WareHouse.Dto;
using Coffee.Core.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application
{
    public interface IWareHouseService
    {
        Task<ListResult<WareHouseDto>> GetListWareHouse(GetWareHouse input);
        Task<List<MaterialDetailWareHouse>> GetListWareHouseDetail(long Id);
        Task<long> CreateExportInvoice(CreateExportInvoiceDto createExportInvoiceDto);

        Task<ListResult<ExportInvoiceDto>> GetListExportInvoice(BaseParamModel baseParam);
    }
}
