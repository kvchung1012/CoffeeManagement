using Coffee.EntityFramworkCore.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.EntityFramworkCore.Model
{
    public class ReviewProduct : BaseEntity
    {
        public int Star { get; set; }
        public int Review { get; set; }
        public int ParentId { get; set; }
    }
}
