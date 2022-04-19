using Coffee.EntityFramworkCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application.Common.Dtos
{
    public class SystemTableColumnDto : SystemTableColumn
    {
        public List<SelectBoxDataDto> SelectBoxData { get; set; }
    }
}
