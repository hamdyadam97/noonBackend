﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Models
{
    public class Specification:BaseEntity
    {
        public string Name { get; set; }    
        
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
