using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Models
{
    public interface IDeletedEntity
    {
        bool IsDeleted { get; set; }
    }
}
