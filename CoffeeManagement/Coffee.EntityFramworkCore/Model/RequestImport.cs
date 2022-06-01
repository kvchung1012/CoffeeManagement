using Coffee.EntityFramworkCore.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.EntityFramworkCore.Model
{
    public class RequestImport : BaseEntity
    {
        [Column(TypeName = "nvarchar(256)")]
        public string Code { get; set; }
        [Column(TypeName = "nvarchar(256)")]
        public string Title { get; set; }
        [Column(TypeName = "nvarchar(256)")]
        public string? Note { get; set; }
    }
}