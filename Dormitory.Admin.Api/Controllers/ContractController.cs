using Dormitory.Admin.Application.Catalog.ContractRepositoty;
using Dormitory.Admin.Application.Catalog.ContractRepositoty.Requests;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dormitory.Admin.Api.Controllers
{
    [Route("api/contract")]
    //[Authorize]
    public class ContractController : Controller
    {
        private readonly IContractRepo _contractRepo;
        public ContractController(IContractRepo contractRepo)
        {
            _contractRepo = contractRepo;
        }
        [HttpGet("get-list")]
        public async Task<IActionResult> GetListContract([FromQuery] PageRequestBase request)
        {
            var list = await _contractRepo.GetList(request);
            return Ok(list);
        }
        [HttpGet("get-list-contract-pending")]
        public async Task<IActionResult> GetListContractPeding([FromQuery] PageRequestBase request)
        {
            var list = await _contractRepo.GetListContractPending(request);
            return Ok(list);
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateContract([FromForm] CreateOrUpdateContractRequest request)
        {
            var responseStatus = "";
            var result = await _contractRepo.Create(request);
            if (result == 1)
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
        public async Task<IActionResult> DeleteContract([FromQuery] int id)
        {
            var responseStatus = "";
            var result = await _contractRepo.Delete(id);
            if (result == 1)
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
