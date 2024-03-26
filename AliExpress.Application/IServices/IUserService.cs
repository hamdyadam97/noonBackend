using AliExpress.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.IServices
{
   public interface IUserService
    {
        Task<AppUserDto> GetUserByIdAsync(string userId);
        List<UserDTO> GetUsers();
    }
}
