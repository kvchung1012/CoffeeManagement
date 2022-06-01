using Coffee.EntityFramworkCore.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.EntityFramworkCore.Model
{
    public class ExportInvoiceDetail : BaseEntity
    {
        public long ExportInvoiceId { get; set; }
        public long WarehouseId { get; set; }
        public long Stock { get; set; }
        public long StockInWarehouse { get; set; }
    }
}
