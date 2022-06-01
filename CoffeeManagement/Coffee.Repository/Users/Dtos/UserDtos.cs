using Coffee.Application.Position.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application.Users.Dtos
{
    public class UserDtos
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public string Identity { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public long Status { get; set; }
        public string StatusName { get; set; }
    }

    public class CreateUserDto
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public long Status { get; set; }
        //public List<CreatePositionUserDto> Position { get; set; } 
    }

    public class CreateUserRole
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
    }

    public class UserRole
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Checked { get; set; }
    }

    public class PositionByUser
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long UserPositionId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
