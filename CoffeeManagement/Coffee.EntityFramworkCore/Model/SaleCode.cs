using Coffee.EntityFramworkCore.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.EntityFramworkCore.Model
{
    public class SaleCode : BaseEntity
    {
        [Required]
        [Column(TypeName = "nvarchar(512)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(256)")]
        public string Code { get; set; }

        public bool SaleType { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Value { get; set; }
        
        public long Stock { get; set; }
        
        public long StockByUser { get; set; }
        
        public DateTime StartTime { get; set; }
        
        public DateTime? EndTime { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? MinPrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? MaxPriceSale { get; set; }
    }
}
