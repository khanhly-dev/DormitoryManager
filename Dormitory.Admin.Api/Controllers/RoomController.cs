using Dormitory.Admin.Application.Catalog.RoomRepository;
using Dormitory.Admin.Application.Catalog.RoomRepository.Requests;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dormitory.Admin.Api.Controllers
{
    [Route("api/room")]
    //[Authorize]
    public class RoomController : Controller
    {
        private readonly IRoomRepo _roomRepo;
        public RoomController(IRoomRepo roomRepo)
        {
            _roomRepo = roomRepo;
        }
        [HttpGet("get-list")]
        public async Task<IActionResult> GetListRoom([FromQuery] PageRequestBase request)
        {
            var list = await _roomRepo.GetList(request);
            return Ok(list);
        }
        [HttpGet("get-list-bill-by-room")]
        public async Task<IActionResult> GetBillByRoom([FromQuery] int roomId)
        {
            var list = await _roomRepo.GetBillByRoom(roomId);
            return Ok(list);
        }
        [HttpGet("get-list-select")]
        public async Task<IActionResult> GetListRoomSelect()
        {
            var list = await _roomRepo.GetListRoomSelect();
            return Ok(list);
        }
        [HttpGet("get-list-empty-room")]
        public async Task<IActionResult> GetListEmptyRoom()
        {
            var list = await _roomRepo.GetListEmptyRoom();
            return Ok(list);
        }
        [HttpPost("create-or-update")]
        public async Task<IActionResult> CreateOrUpdateRoom([FromForm] CreateOrUpdateRoomRequest request)
        {
            var responseStatus = "";
            var result = await _roomRepo.CreateOrUpdate(request);
            if (result > 0)
            {
                responseStatus = "success";
            }
            else
            {
                responseStatus = "error";
            }
            return Ok(new { responseStatus });
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteRoom([FromQuery] int id)
        {
            var responseStatus = "";
            var result = await _roomRepo.Delete(id);
            if (result > 0)
            {
                responseStatus = "success";
            }
            else
            {
                responseStatus = "error";
            }
            return Ok(new { responseStatus });
        }
    }
}
