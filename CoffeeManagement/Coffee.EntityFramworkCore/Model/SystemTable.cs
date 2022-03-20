using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.EntityFramworkCore.Model
{
    public class SystemTable
    {
        public long Id { get; set; }
        public string TableName { get; set; }
        public string SqlTableName { get; set; }
        public string SqlAlias { get; set; }
        public bool IsDeleted { get;set; }
    }
}
