using AliExpress.Application.Contract;
using AliExpress.Context;
using AliExpress.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace None.Infrastructure
{
    public class TransactionRepository : Repoditory<Transaction, int>, ITransactionRepository
    {
        private readonly AliExpressContext _context;

        public TransactionRepository(AliExpressContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CoutTransaction()
        {
            int counts = await _context.Transactions.CountAsync();
            return counts;
        }

      
    }
}
