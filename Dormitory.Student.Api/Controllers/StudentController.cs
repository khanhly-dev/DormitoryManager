using Dormitory.Student.Application.Catalog.StudentRepository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dormitory.Student.Api.Controllers
{
    [Route("api/student")]
    public class StudentController : Controller
    {
        private readonly IStudentRepo _studentRepo;
        public StudentController(IStudentRepo studentRepo)
        {
            _studentRepo = studentRepo;
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUser()
        {
            var listStudent = await _studentRepo.GetAllStudent();
            return Ok(listStudent);
        }
    }
}
