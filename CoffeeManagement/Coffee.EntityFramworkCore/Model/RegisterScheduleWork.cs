﻿using Coffee.EntityFramworkCore.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.EntityFramworkCore.Model
{
    public class RegisterScheduleWork : BaseEntity
    {
        public long ShiftId { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}