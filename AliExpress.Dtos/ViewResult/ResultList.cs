using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Dtos.ViewResult
{
    public class ResultList<TEntity>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<TEntity> Entities { get; set; }
        public int Count { get; set; }
        public ResultList()
        {
            Entities = new List<TEntity>();
        }
    }
}
