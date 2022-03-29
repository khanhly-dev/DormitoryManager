using Dormitory.Admin.Application.Catalog.ServiceRepository;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dormitory.Admin.Api.Controllers
{
    [Route("api/service")]
    [Authorize]
    public class ServiceController : Controller
    {
        private readonly IServiceRepo _serviceRepo;
        public ServiceController(IServiceRepo serviceRepo)
        {
            _serviceRepo = serviceRepo;
        }
        [HttpGet("get-list")]
        public async Task<IActionResult> GetListService([FromQuery] PageRequestBase request)
        {
            var list = await _serviceRepo.GetList(request);
            return Ok(list);
        }
        [HttpPost("create-or-update")]
        public async Task<IActionResult> CreateOrUpdateService([FromForm] ServiceEntity request)
        {
            var responseStatus = "";
            var result = await _serviceRepo.CreateOrUpdate(request);
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
        public async Task<IActionResult> DeleteService([FromQuery] int id)
        {
            var responseStatus = "";
            var result = await _serviceRepo.Delete(id);
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
