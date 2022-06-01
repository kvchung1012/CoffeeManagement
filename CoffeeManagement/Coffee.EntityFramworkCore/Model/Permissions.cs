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
    public class Permissions : BaseEntity
    {
        [Required]
        [Column(TypeName = "nvarchar(256)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(1024)")]
        public string? Description { get; set; }
        public long ParentId { get; set; }
    }
}
