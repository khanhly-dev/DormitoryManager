using Dormitory.Core.Application.Catalog.CoreRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dormitory.Core.Api.Controllers
{
    [Route("api/core")]
    public class CoreController : Controller
    {
        private readonly ICoreRepo _coreRepo;
        public CoreController(ICoreRepo coreRepo)
        {
            _coreRepo = coreRepo;
        }
        [HttpGet("login")]
        public async Task<IActionResult> Login(string userName, string password, int tenantId)
        {
            var access_token = await _coreRepo.Authenticate(userName, password, tenantId);
            return Ok(access_token);
        }

        [HttpGet("test")]
        [Authorize]
        public async Task<IActionResult> test()
        {
            var t = "test authen";
            return Ok(t);
        }
    }
}
