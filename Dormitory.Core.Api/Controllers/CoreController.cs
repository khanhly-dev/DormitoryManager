using Dormitory.Core.Application.Catalog.CoreRepository;
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
            var access_token = await _coreRepo.Authenticate(userName, password, tenantId);
            return Ok(new { access_token = access_token });
        }
    }
}
