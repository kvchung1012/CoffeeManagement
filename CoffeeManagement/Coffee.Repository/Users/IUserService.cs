using Coffee.Application.Users.Dtos;
using Coffee.Core.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application
{
    public interface IUserService
    {
        public Task<ListResult<UserDtos>> GetListUser(BaseParamModel baseParam);
        public Task<List<UserRole>> GetListRoleByUser(long Id);
        public Task<long> CreateUserRole(List<CreateUserRole> userRoles);
        public Task<List<UserDtos>> GetAllStaff();
        public Task<long> CreateUser(CreateUserDto userDto);
        public Task<UserDtos> GetUserById(long id);
        public Task<List<PositionByUser>> GetPositionByUserId(long id);
    }
}
