using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Dtos.User
{
    public class RestChangePassword
    {
        public string Email { get; set; }
        public string Code { get; set; }
       
        public string Password { get; set; }
    }
}
