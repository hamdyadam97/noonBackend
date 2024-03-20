using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace AliExpress.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }
        public List<CartItem>? Items { get; set; } = new List<CartItem>();
        public decimal? TotalAmount { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
