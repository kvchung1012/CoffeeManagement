using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application.ImportInvoice.Dto
{
    public class ImportInvoiceDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime CreatedTime { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public long Status { get; set; }
        public string StatusName { get; set; }
    }

    public class CreateImportInvoice
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public long SupplierId { get; set; }
        public long Status { get; set; }
        public List<ImportInvoiceDetail> ImportInvoiceDetails { get; set; }

    }

    public class ImportInvoiceDetail
    {
        public long Id { get; set; }
        public long ImportInvoiceId { get; set; }
        public long MaterialId { get; set; }
        public long Stock { get; set; }
        public decimal Price { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime ExpriedTime { get; set; }
    }
}
