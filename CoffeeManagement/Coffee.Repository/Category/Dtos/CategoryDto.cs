using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application.Category.Dtos
{
    public class CategoryDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long Status { get; set; }
    }
}
