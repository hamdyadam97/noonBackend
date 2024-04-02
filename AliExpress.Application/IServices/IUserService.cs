using AliExpress.Dtos.User;
using AliExpress.Dtos.ViewResult;
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
        Task<ResultView<APIUserDTO>> Update(APIUserDTO userDTO);

    }
}
