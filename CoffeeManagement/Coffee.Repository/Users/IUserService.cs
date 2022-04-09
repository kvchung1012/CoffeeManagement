using Coffee.Application.Users.Dtos;
using Coffee.Core.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application.Users
{
    public interface IUserService
    {
       public Task<(List<UserDtos>, int)> GetListUser(BaseParamModel baseParam);
    }
}
