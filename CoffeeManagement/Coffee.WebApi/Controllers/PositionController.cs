using Coffee.Application;
using Coffee.Application.Position.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Coffee.WebApi.Controllers
{
    public class PositionController : BaseController
    {
        public ILogger<PositionController> _logger;
        private readonly IPositionService _positionService;
        public PositionController(ILogger<PositionController> logger
                                , IPositionService positionService)
        {
            _logger = logger;
            _positionService = positionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListPosition()
        {
            var result = await _positionService.GetListPosition();
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetListPositionById(long Id)
        {
            var position = new PositionDetailDto();
            position.postion = await _positionService.GetListPositionById(Id);
            position.staffs = await _positionService.GetListStaffByPosition(Id);
            return Ok(position);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUpdate(PositionDto positionDto)
        {
            var result = await _positionService.CreateOrUpdate(positionDto);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CheckStaffPosition(CreatePositionUserDto positionUserDto)
        {
            return Ok(await CheckCanAddStaffPosition(positionUserDto));
        }

        [HttpPost]
        public async Task<IActionResult> CreateStaffPosition(CreatePositionUserDto positionUserDto)
        {
            if (await CheckCanAddStaffPosition(positionUserDto))
            {
                var res = await _positionService.CreateOrUpdateStaffPosition(positionUserDto);
                return Ok(res);
            }
            return Ok(0);
        }

        private async Task<bool> CheckCanAddStaffPosition(CreatePositionUserDto positionUserDto)
        {
            var result = await _positionService.GetCurrentStaffPosition(positionUserDto.UserId);
            // chưa từng làm ở đâu 
            if (result is null) return true;

            // trường hợp đang làm
            if (result.EndTime == null)
                return false;

            // trường gợp chọn ngày bắt đầu < ngày kết thúc việc hiện tại
            if (result.EndTime >= positionUserDto.StartTime)
                return false;
            return true;
        }
    }
}
