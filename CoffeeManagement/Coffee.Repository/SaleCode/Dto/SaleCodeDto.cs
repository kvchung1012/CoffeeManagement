using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application.SaleCode.Dto
{
    public class SaleCodeDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool SaleType { get; set; }
        public decimal Value { get; set; }
        public long Stock { get; set; }
        public long StockByUser { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPriceSale { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public long UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }
        public long Status { get; set; }
        public string StatusName { get; set; }
    }

    public class CreateSaleCodeDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool SaleType { get; set; }
        public decimal Value { get; set; }
        public long Stock { get; set; }
        public long StockByUser { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPriceSale { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public long Status { get; set; }
    }
}
