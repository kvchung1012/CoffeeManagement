using Coffee.EntityFramworkCore.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.EntityFramworkCore.Model
{
    public class MasterData : BaseEntity
    {
        public string Code { get; set; }
        public string Value { get; set; }
        public long ParentId { get; set; }
    }
}
