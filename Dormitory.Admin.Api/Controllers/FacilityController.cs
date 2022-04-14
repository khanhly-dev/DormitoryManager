using Dormitory.Admin.Application.Catalog.FacilityRepository;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dormitory.Admin.Api.Controllers
{
    [Route("api/facility")]
    //[Authorize]
    public class FacilityController : Controller
    {
        private readonly IFacilityRepo _facilityRepo;
        public FacilityController(IFacilityRepo facilityRepo)
        {
            _facilityRepo = facilityRepo;
        }
        [HttpGet("get-list")]
        public async Task<IActionResult> GetListFacility([FromQuery] PageRequestBase request)
        {
            var list = await _facilityRepo.GetList(request);
            return Ok(list);
        }
        [HttpPost("create-or-update")]
        public async Task<IActionResult> CreateOrUpdateFacility([FromForm] FacilityEntity request)
        {
            var responseStatus = "";
            var result = await _facilityRepo.CreateOrUpdate(request);
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
        public async Task<IActionResult> DeleteFacility([FromQuery] int id)
        {
            var responseStatus = "";
            var result = await _facilityRepo.Delete(id);
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

        [HttpGet("get-list-facility-in-room")]
        public async Task<IActionResult> GetListFacilityInRoom()
        {
            var list = await _facilityRepo.GetListFacilityInRoom();
            return Ok(list);
        }
        [HttpGet("get-list-select")]
        public async Task<IActionResult> GetListFacilitySelect()
        {
            var list = await _facilityRepo.GetListSelect();
            return Ok(list);
        }
        [HttpGet("get-list-facility-by-room")]
        public async Task<IActionResult> GetListFacilityByRoom([FromQuery] int roomId)
        {
            var list = await _facilityRepo.GetFacilityByRoomId(roomId);
            return Ok(list);
        }
        [HttpPost("add-facility-into-room")]
        public async Task<IActionResult> AddFacilityIntoRoom([FromForm] FacilityInRoomEntity request)
        {
            var responseStatus = "";
            var result = await _facilityRepo.AddFacilityIntoRoom(request);
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
        [HttpDelete("delete-facility-in-room")]
        public async Task<IActionResult> DeleteFacilityInRoom([FromQuery] int id)
        {
            var responseStatus = "";
            var result = await _facilityRepo.DeleteFacilityInRoom(id);
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
