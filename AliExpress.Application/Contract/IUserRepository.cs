﻿using AliExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.Contract
{
    public interface IUserRepository
    {
        Task<AppUser> GetUserByIdAsync(string userId);
    }
}
