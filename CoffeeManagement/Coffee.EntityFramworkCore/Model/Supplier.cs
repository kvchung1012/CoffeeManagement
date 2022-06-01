using Coffee.EntityFramworkCore.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.EntityFramworkCore.Model
{
    public class Supplier : BaseEntity
    {
        [Required]
        [Column(TypeName = "nvarchar(256)")]
        public string Code { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(512)")]
        public string Name { get; set; }
    }
}
