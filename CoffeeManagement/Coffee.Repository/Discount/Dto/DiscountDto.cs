using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application.Discount.Dto
{
    public class DiscountDto
    {
        public long Id { get; set; }
        public bool SaleType { get; set; }
        public decimal Value { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public long Status { get; set; }
        public string StatusName { get; set; }
    }
}
