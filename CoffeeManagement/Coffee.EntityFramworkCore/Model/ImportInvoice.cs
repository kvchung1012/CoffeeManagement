using Coffee.EntityFramworkCore.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.EntityFramworkCore.Model
{
    public class ImportInvoice : BaseEntity
    {
        public long SupplierId { get; set; }
        public decimal TotalCost { get; set; }
    }
}
