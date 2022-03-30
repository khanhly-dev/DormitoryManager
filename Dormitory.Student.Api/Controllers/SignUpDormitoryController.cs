using Dormitory.Student.Application.Catalog.SignUpDormitory;
using Dormitory.Student.Application.Catalog.SignUpDormitory.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dormitory.Student.Api.Controllers
{
    [Route("api/contract")]
    //[Authorize]
    public class SignUpDormitoryController : Controller
    {
        private readonly ISignUpDormitoryRepo _signUpDormitoryRepo;
        public SignUpDormitoryController(ISignUpDormitoryRepo signUpDormitoryRepo)
        {
            _signUpDormitoryRepo = signUpDormitoryRepo;
        }
        [HttpPost("sgin-up")]
        public async Task<IActionResult> SignUpDomitory([FromForm] SignUpRequest request)
        {
            var responseStatus = "";
            var result = await _signUpDormitoryRepo.SignUp(request);
            if(result > 0)
            {
                responseStatus = "success";
            }
            else
            {
                responseStatus = "error";
            }    
            return Ok(new { responseStatus });
        }
        [HttpPost("set-student-point")]
        public async Task<IActionResult> SetStudentPoint([FromForm] SetStudentPointRepuest request)
        {
            var responseStatus = "";
            var result = await _signUpDormitoryRepo.SetStudentPoint(request);
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
        [HttpGet("get-list-criteria")]
        public async Task<IActionResult> GetListCriteria()
        {
            var listCriteria = await _signUpDormitoryRepo.GetListCriteria();
            return Ok(listCriteria);
        }
    }
}
