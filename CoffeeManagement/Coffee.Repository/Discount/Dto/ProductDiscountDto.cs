using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application.Discount.Dto
{
    public class ProductDiscountDto
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long DiscountId { get; set; }
    }
}
