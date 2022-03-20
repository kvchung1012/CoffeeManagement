using System.Collections.Generic;

namespace Coffee.WebApi.ViewModel
{
    public class ListResult<T>
    {
        public List<T> Results { get; set; }
        public int TotalCount { get; set; }
    }
}
