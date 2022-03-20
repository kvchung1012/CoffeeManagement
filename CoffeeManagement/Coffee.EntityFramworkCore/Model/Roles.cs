using Coffee.EntityFramworkCore.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.EntityFramworkCore.Model
{
    public class Roles : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }

    }
}
