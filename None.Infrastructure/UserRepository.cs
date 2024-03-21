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
    public class UserRepository : IUserRepository
    {
        private readonly AliExpressContext _context;

        public UserRepository(AliExpressContext context)
        {
            _context = context;
        }

        public async Task<AppUser> GetUserByIdAsync(string userId)
        {
            return await _context.Users.FindAsync(userId);
        }
    }
}
