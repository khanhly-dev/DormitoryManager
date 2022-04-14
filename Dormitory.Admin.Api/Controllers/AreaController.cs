using Dormitory.Admin.Application.Catalog.AreaRepository;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dormitory.Admin.Api.Controllers
{
    [Route("api/area")]
    //[Authorize]
    public class AreaController : Controller
    {
        private readonly IAreaRepo _areaRepo;
        public AreaController(IAreaRepo areaRepo)
        {
            _areaRepo = areaRepo;
        }
        [HttpGet("get-list")]
        public async Task<IActionResult> GetListArea([FromQuery] PageRequestBase request)
        {
            var listArea = await _areaRepo.GetList(request);
            return Ok(listArea);
        }
        [HttpGet("get-list-select")]
        public async Task<IActionResult> GetListAreaSelect()
        {
            var listArea = await _areaRepo.GetListAreaSelect();
            return Ok(listArea);
        }
        [HttpPost("create-or-update")]
        public async Task<IActionResult> CreateOrUpdateArea([FromForm] AreaEntity request)
        {
            var responseStatus = "";
            var result = await _areaRepo.CreateOrUpdate(request);
            if(result > 0)
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
        public async Task<IActionResult> DeleteArea([FromQuery] int id)
        {
            var responseStatus = "";
            var result = await _areaRepo.Delete(id);
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
