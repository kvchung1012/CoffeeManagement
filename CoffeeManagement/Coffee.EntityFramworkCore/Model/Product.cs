using Coffee.EntityFramworkCore.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.EntityFramworkCore.Model
{
    public class Product : BaseEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsCombo { get; set; }
        public bool IsTop { get; set; }
    }
}
