using Coffee.EntityFramworkCore.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.EntityFramworkCore.Model
{
    public class Discount : BaseEntity
    {
        public bool SaleType { get; set; }
        public decimal Value { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
