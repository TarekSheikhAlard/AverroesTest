﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Domain.Models
{
    public class ErrorModel
    {
        public bool IsAuthenticated { get; set; }
        public string Message { get; set; }    
    }
}
