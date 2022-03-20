using Coffee.Application.Auth.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application.Auth
{
    public interface IAuthService
    {
        public Task<EntityFramworkCore.Model.Users> GetUserByUserName(UserLoginDto input);
    }
}
