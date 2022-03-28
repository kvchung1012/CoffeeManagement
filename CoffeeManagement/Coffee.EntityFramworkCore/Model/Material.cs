using Coffee.EntityFramworkCore.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.EntityFramworkCore.Model
{
    public class Material : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public long Unit { get; set; }
    }
}
