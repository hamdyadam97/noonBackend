using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Dtos.Pagination
{
    public class PaginationResult<T>
    {
        public IEnumerable<T> Entities { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int numberOfPages { get; set; }

        public PaginationResult(IEnumerable<T> entities, int pageIndex, int pageSize, int totalCount,bool isSuccess,string message)
        {
            IsSuccess = isSuccess;
            Message = message;
            Entities = entities;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }
}
