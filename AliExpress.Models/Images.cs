using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Models

{
    public class Images:BaseEntity
    {
        public string Url { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
       
    }
}
