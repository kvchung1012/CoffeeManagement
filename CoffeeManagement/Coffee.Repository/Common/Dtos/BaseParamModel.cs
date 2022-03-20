using Coffee.Application.Auth;
using Coffee.Core.Service;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application.Common.Dtos
{
    public class BaseParamModel
    {
        public string TableConfigName { get; set; }
        public string FilterString { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int SortBy { get; set; }
        public bool IsAsc { get; set; }
        public List<FilterColumn> filterColumns { get; set; }
    }
}
