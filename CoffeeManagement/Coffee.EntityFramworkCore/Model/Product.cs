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
    public class Product : BaseEntity
    {
        [Required]
        [Column(TypeName = "nvarchar(256)")]
        public string Code { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(512)")]
        public string Name { get; set; }
        public long CategoryId { get; set; }

        [Column(TypeName = "nvarchar(1024)")]
        public string? Description { get; set; }

        [Column(TypeName = "nvarchar(512)")]
        public string? Image { get; set; }
        public bool IsCombo { get; set; }
        public bool IsTop { get; set; }
    }
}
