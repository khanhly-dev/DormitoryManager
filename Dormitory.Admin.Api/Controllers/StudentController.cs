using Dormitory.Admin.Application.Catalog.StudentRepository;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dormitory.Admin.Api.Controllers
{
    [Route("api/student")]
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IStudentRepo _studentRepo;
        public StudentController(IStudentRepo studentRepo)
        {
            _studentRepo = studentRepo;
        }
        [HttpGet("get-list")]
        public async Task<IActionResult> GetListStudent(PageRequestBase request)
        {
            var listArea = await _studentRepo.GetList(request);
            return Ok(listArea);
        }
        [HttpPost("create-or-update")]
        public async Task<IActionResult> CreateOrUpdateStudent(StudentEntity request)
        {
            var responseStatus = "";
            var result = await _studentRepo.CreateOrUpdate(request);
            if (result > 0)
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
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var responseStatus = "";
            var result = await _studentRepo.Delete(id);
            if (result > 0)
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
