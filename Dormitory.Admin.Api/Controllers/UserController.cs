using Dormitory.Admin.Application.Catalog.UserRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dormitory.Admin.Api.Controllers
{
    [Route("api/user")]
    //[Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepo _userRepo;
        public UserController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUser()
        {
            var listUser = await _userRepo.GetAllUser();
            return Ok(listUser);
        }
    }
}
