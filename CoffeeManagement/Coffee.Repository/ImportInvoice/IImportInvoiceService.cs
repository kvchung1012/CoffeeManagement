using Coffee.Application.ImportInvoice.Dto;
using Coffee.Core.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application
{
    public interface IImportInvoiceService
    {
        public Task<ListResult<ImportInvoiceDto>> GetListImportInvoice(BaseParamModel baseParam);
        public Task<long> CreateImportInvoice(CreateImportInvoice createImport);
    }
}
