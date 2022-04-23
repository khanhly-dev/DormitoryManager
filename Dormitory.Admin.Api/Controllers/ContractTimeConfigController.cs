using Dormitory.Admin.Application.Catalog.ContractTimeConfigRepository;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dormitory.Admin.Api.Controllers
{
    [Route("api/contract-time-config")]
    //[Authorize]
    public class ContractTimeConfigController : Controller
    {
        private readonly IContractTimeConfigRepo _contractTimeConfigRepo;
        public ContractTimeConfigController(IContractTimeConfigRepo contractTimeConfigRepo)
        {
            _contractTimeConfigRepo = contractTimeConfigRepo;
        }
        [HttpGet("get-list")]
        public async Task<IActionResult> GetListContractConfig([FromQuery] PageRequestBase request)
        {
            var listArea = await _contractTimeConfigRepo.GetList(request);
            return Ok(listArea);
        }
        [HttpGet("get-list-select")]
        public async Task<IActionResult> GetListContractConfigSelect()
        {
            var listArea = await _contractTimeConfigRepo.GetListSelect();
            return Ok(listArea);
        }
        [HttpPost("create-or-update")]
        public async Task<IActionResult> CreateOrUpdateContractConfig([FromForm] ContractTimeConfigEntity request)
        {
            var responseStatus = "";
            var result = await _contractTimeConfigRepo.CreateOrUpdate(request);
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
        public async Task<IActionResult> DeleteContractConfig([FromQuery] int id)
        {
            var responseStatus = "";
            var result = await _contractTimeConfigRepo.Delete(id);
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
