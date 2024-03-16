using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Models
{
    public class CartItem
    {
     public int CartItemId { get; set; }
     public int Quantity { get; set; }
    //productRelation-one
    public int ProductId { get; set; }
    public Product Product { get; set; }

    //cartRelation-one
     public int CartId { get; set; }
     public Cart Cart { get; set; }
    }
}
