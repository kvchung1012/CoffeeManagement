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
    public class ReviewProduct : BaseEntity
    {
        public int Star { get; set; }

        [Column(TypeName = "nvarchar(256)")]
        public string? Review { get; set; }
        public int ParentId { get; set; }
    }
}
