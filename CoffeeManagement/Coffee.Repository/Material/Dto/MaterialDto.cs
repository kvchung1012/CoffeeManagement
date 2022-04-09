using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application.Material.Dto
{
    public class MaterialDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public DateTime CreatedTime { get; set; }
        public long Status { get; set; }
    }
}
