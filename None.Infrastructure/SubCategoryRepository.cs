using AliExpress.Application.Contract;
using AliExpress.Context;
using AliExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace None.Infrastructure
{
    public class SubCategoryRepository:Repoditory<Subcategory,int> ,ISubCategoryRepository
    {
        private readonly AliExpressContext _context;
       

        public SubCategoryRepository(AliExpressContext context):base(context)
        {
            _context = context;
          
        }

    }
}
