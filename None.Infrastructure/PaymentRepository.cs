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
    public class PaymentRepository : Repoditory<PaymentMethod, int>, IPaymentRepository
    {
        private readonly AliExpressContext _context;

        public PaymentRepository(AliExpressContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CoutPayemnt()
        {
            return await _context.PaymentMethods.CountAsync();
        }

       

    }
}
