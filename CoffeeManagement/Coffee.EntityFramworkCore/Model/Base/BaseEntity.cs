﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.EntityFramworkCore.Model.Base
{
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public DateTime? CreatedTime { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public long? UpdatedBy { get; set; }
        public long? Status { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
