using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Dtos.User
{
    public class ChangePassword
    {
        public string Email { get; set; }
       
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
