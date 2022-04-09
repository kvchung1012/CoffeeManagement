using Coffee.EntityFramworkCore.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.EntityFramworkCore.Model
{
    public class ImportInvoiceDetail : BaseEntity
    {
        public long ImportInvoiceId { get; set; }
        public long MaterialId { get; set; }
        public long Stock { get; set; }
        public decimal Price { get; set; }
        public long TotalPrice { get; set; }
    }
}
