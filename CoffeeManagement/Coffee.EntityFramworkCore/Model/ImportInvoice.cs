using Coffee.EntityFramworkCore.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.EntityFramworkCore.Model
{
    public class ImportInvoice : BaseEntity
    {
        [Column(TypeName = "nvarchar(256)")]
        public string Code { get; set; }
        
        [Column(TypeName = "nvarchar(256)")]
        public string Description { get; set; }
        
        [Column(TypeName = "nvarchar(1024)")]
        public string Note { get; set; }
        
        public long SupplierId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalCost { get; set; }
    }
}
