﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.Customer
{
    public class ChangePassDTO
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
