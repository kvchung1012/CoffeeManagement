using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Core.BaseModel
{
    public class ListResult<T>
    {
        public List<T> Result { get; set; }
        public long Count { get; set; }

        public ListResult()
        {
            Result = new List<T>();
            Count = 0;
        }

        public ListResult(List<T> results, long totalCount)
        {
            Result = results;
            Count = totalCount;
        }
    }
}
