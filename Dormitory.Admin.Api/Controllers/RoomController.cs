using Dormitory.Admin.Application.Catalog.RoomRepository;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
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
        public async Task<IActionResult> GetListRoom(PageRequestBase request)
        {
            var listArea = await _roomRepo.GetList(request);
            return Ok(listArea);
        }
        [HttpPost("create-or-update")]
        public async Task<IActionResult> CreateOrUpdateRoom(RoomEntity request)
        {
            var responseStatus = "";
            var result = await _roomRepo.CreateOrUpdate(request);
            if (result == 1)
            {
                responseStatus = "success";
            }
            else
            {
                responseStatus = "error";
            }
            return Ok(responseStatus);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var responseStatus = "";
            var result = await _roomRepo.Delete(id);
            if (result == 1)
            {
                responseStatus = "success";
            }
            else
            {
                responseStatus = "error";
            }
            return Ok(responseStatus);
        }
    }
}
