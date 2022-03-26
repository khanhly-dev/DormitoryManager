using Dormitory.Admin.Application.Catalog.ServiceRepository;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dormitory.Admin.Api.Controllers
{
    [Route("api/service")]
    //[Authorize]
    public class ServiceController : Controller
    {
        private readonly IServiceRepo _serviceRepo;
        public ServiceController(IServiceRepo serviceRepo)
        {
            _serviceRepo = serviceRepo;
        }
        [HttpGet("get-list")]
        public async Task<IActionResult> GetListService(PageRequestBase request)
        {
            var list = await _serviceRepo.GetList(request);
            return Ok(list);
        }
        [HttpPost("create-or-update")]
        public async Task<IActionResult> CreateOrUpdateService(ServiceEntity request)
        {
            var responseStatus = "";
            var result = await _serviceRepo.CreateOrUpdate(request);
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
        public async Task<IActionResult> DeleteService(int id)
        {
            var responseStatus = "";
            var result = await _serviceRepo.Delete(id);
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
