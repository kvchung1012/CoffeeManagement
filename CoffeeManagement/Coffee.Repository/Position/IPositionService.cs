using Coffee.Application.Position.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application
{
    public interface IPositionService
    {
        public Task<List<PositionDto>> GetListPosition();
        public Task<PositionDto> GetListPositionById(long Id);
        public Task<List<PositionUserDto>> GetListStaffByPosition(long Id);
        public Task<long> CreateOrUpdate(PositionDto position);
        public Task<PositionUserDto> GetCurrentStaffPosition(long staffId);
        public Task<long> CreateOrUpdateStaffPosition(CreatePositionUserDto position);
    }
}
