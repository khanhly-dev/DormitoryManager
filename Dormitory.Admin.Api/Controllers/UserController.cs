using Dormitory.Admin.Application.Catalog.UserRepository;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
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
        [HttpGet("get-list")]
        public async Task<IActionResult> GetListUser(PageRequestBase request)
        {
            var listUser = await _userRepo.GetListUser(request);
            return Ok(listUser);
        }
        [HttpGet("get-by-user")]
        public async Task<IActionResult> GetAccountByUser(int userInfoId, int tenant)
        {
            var listUser = await _userRepo.GetUserAccountByInfo(userInfoId, tenant);
            return Ok(listUser);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var listUser = await _userRepo.DeleteUser(id);
            return Ok(listUser);
        }
        [HttpDelete("delete-account")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var listUser = await _userRepo.DeleteAccount(id);
            return Ok(listUser);
        }
        [HttpPost("create-or-update")]
        public async Task<IActionResult> CreateOrUpdate(UserInfoEntity request)
        {
            var listUser = await _userRepo.CreateOrUpdateUser(request);
            return Ok(listUser);
        }

    }
}
