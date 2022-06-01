using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application.Position.Dto
{

    // danh sách vị trí
    public class PositionDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long ParentId { get; set; }
    }

    // lấy ra người dùng trong chức vụ
    public class PositionUserDto
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public long UserPositionId { get; set; }
        public long PositionId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }

    //public class CreatePosition

    // cập nhật người dùng trong chức vụ đấy
    public class CreatePositionUserDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long PositionId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }

    public class PositionDetailDto
    {
        public PositionDto postion { get; set; }
        public List<PositionUserDto> staffs { get; set; }
    }
}
