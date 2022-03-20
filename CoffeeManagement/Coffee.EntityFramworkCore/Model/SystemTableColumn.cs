using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.EntityFramworkCore.Model
{
    public class SystemTableColumn
    {
        public long Id { get; set; }
        public string TableId { get; set; }
        public string ColumnName { get; set; } // tên sau khi join
        public string SqlColumnName { get; set; } // tên nguyên bản trong table
        public string SqlAlias { get; set; } 
        public string ColumnHeader { get; set; } // tên hiển thị 
        public int DataTypeId { get; set; }
        public bool IsShow { get; set; }
        public bool IsFilter { get; set; }
        public bool IsDeleted { get; set; }
    }
}
