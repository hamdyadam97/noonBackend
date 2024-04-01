using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Dtos.User
{
    public class APIUserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; } // Make sure there is a setter for PhoneNumber

        public string FName { get; set; }
        public string LName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string National { get; set; }
    }
}
