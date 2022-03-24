using Dormitory.Core.Application.Catalog.CoreRepository;
using Dormitory.Core.Application.Catalog.CoreRepository.Dtos;
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
        public CoreController(ICoreRepo coreRepo)
        {
            _coreRepo = coreRepo;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] string userName, [FromForm] string password, [FromForm] int tenantId)
        {
            var loginInfo = await _coreRepo.Authenticate(userName, password, tenantId);
            return Ok(loginInfo);
 
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
            return Ok(status);

        }
    }
}
