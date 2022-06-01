using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application.WareHouse.Dto
{
    public class WareHouseDto
    {
        public long Id { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public string UnitName { get; set; }
        public long Stock { get; set; }
    }

    public class MaterialDetailWareHouse
    {
        public long Id { get; set; }
        public long MaterialId { get; set; }
        public string Code { get; set; }
        public long Stock { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpriedDate { get; set; }
    }

    public class GetWareHouse
    {
        public long MaterialId { get; set; }
        public long Skip { get; set; }
        public long Take { get; set; }
    }


    // xuất kho
    public class ExportInvoiceDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public long ExportTo { get; set; }
        public string ExportToName { get; set; }
        public string CreatedTime { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public long Status { get; set; }
        public string StatusName { get; set; }
    }

    public class ExportInvoiceDetailDto
    {
        public long Id { get; set; }
        public long ExportInvoiceId { get; set; }
        public long WarehouseId { get; set; }
        public long MaterialId { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public long Stock { get; set; }
        public long StockInWarehouse { get; set; }
    }

    public class CreateExportInvoiceDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public long ExportTo { get; set; }
        public List<ExportInvoiceDetailDto> ExportInvoiceDetails { get; set; }
    }
}
