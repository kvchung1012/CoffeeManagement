using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application.Dashboard.Dto
{
    public class DashboardDto
    {
    }

    public class TotalSystem
    {
        public long Users { get; set; }
        public long Products { get; set; }
        public long Orders { get; set; }
        public long OrderToday { get; set; }
        public long Supplier { get; set; }
    }
}
