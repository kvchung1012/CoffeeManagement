using Coffee.Application.Position.Dto;
using Coffee.Core.DbManager;
using Dapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application
{
    public class PositionService : IPositionService
    {
        private readonly IDbManager _db;
        private readonly IHttpContextAccessor _httpContext;
        public PositionService(IDbManager db, IHttpContextAccessor httpContext)
        {
            _db = db;
            _httpContext = httpContext;
        }
        public async Task<List<PositionDto>> GetListPosition()
        {
            var par = new DynamicParameters();
            return await _db.QueryAsync<PositionDto>("Sp_Get_GetPosition", par);
        }

        public async Task<PositionDto> GetListPositionById(long Id)
        {
            var par = new DynamicParameters();
            par.Add("@Id", Id);
            return await _db.QueryFirstOrDefaultAsync<PositionDto>("Sp_Get_GetPositionById", par);
        }

        public async Task<List<PositionUserDto>> GetListStaffByPosition(long Id)
        {
            var par = new DynamicParameters();
            par.Add("@Id", Id);
            return await _db.QueryAsync<PositionUserDto>("Sp_Get_GetListStaffByPosition", par);
        }

        public async Task<long> CreateOrUpdate(PositionDto position)
        {
            var par = new DynamicParameters();
            par.AddOutputId(position.Id);
            par.Add("@Name", position.Name);
            par.Add("@Description", position.Description);
            par.Add("@ParentId", position.ParentId);
            par.AddCreatedByDefault(_httpContext);
            var result = await _db.ExecuteAsync("Sp_Create_Position", par);
            return par.GetOutputId();
        }

        public async Task<PositionUserDto> GetCurrentStaffPosition(long Id)
        {
            var par = new DynamicParameters();
            par.Add("@Id", Id);
            return await _db.QueryFirstOrDefaultAsync<PositionUserDto>("Sp_Get_GetCurrentPositionStaff", par);
        }

        public async Task<long> CreateOrUpdateStaffPosition(CreatePositionUserDto position)
        {
            var par = new DynamicParameters();
            par.AddOutputId(position.Id);
            par.Add("@UserId", position.UserId);
            par.Add("@PositionId", position.PositionId);
            par.Add("@StartTime", position.StartTime);
            par.Add("@EndTime", position.EndTime);
            par.AddCreatedByDefault(_httpContext);
            var result = await _db.ExecuteAsync("Sp_Create_StaffPosition", par);
            return par.GetOutputId();
        }
    }
}
