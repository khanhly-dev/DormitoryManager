using Dormitory.Core.Application.Catalog.CoreRepository;
using Dormitory.Core.Application.Catalog.CoreRepository.Dtos;
using Dormitory.Core.Application.Catalog.ProcessRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dormitory.Core.Api.Controllers
{
    [Route("api/core")]
    [Produces("application/json")]
    public class CoreController : Controller
    {
        private readonly ICoreRepo _coreRepo;
        private readonly IProcessRepo _processRepo;

        public CoreController(ICoreRepo coreRepo, IProcessRepo processRepo)
        {
            _coreRepo = coreRepo;
            _processRepo = processRepo;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] string userName, [FromForm] string password, [FromForm] int tenantId)
        {
            var loginInfo = await _coreRepo.Authenticate(userName, password, tenantId);
            return Ok(loginInfo);
        }
        [HttpPut("process-update-room")]
        public async Task<IActionResult> ProcessUpdateRoom()
        {
            var status = "";
            var result = await _processRepo.ProcessUpdateRoom();
            if (result > 0)
            {
                status = "success";
            }
            else
            {
                status = "No room was updated";
            }
            return Ok(new { status });
        }
        [HttpPut("process-update-contract-type")]
        public async Task<IActionResult> ProcessUpdateContractType()
        {
            var status = "";
            var result = await _processRepo.ProcessUpdateContractType();
            if (result > 0)
            {
                status = "success";
            }
            else
            {
                status = "No contract was updated";
            }
            return Ok(new { status });
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterRequest request)
        {
            var status = "";
            var registerStatus = await _coreRepo.Register(request);
            if(registerStatus == 1)
            {
                status = "success";
            }
            else
            {
                status = "error";
            }
            return Ok(new { status });

        }
    }
}
