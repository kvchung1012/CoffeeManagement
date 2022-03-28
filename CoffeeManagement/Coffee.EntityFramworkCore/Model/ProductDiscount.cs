﻿using Coffee.EntityFramworkCore.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.EntityFramworkCore.Model
{
    public class ProductDiscount : BaseEntity
    {
        public long ProductId { get; set; }
        public long DiscountId { get; set; }
    }
}
