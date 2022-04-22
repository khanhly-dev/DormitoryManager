using Dormitory.Domain.AppEntities;
using Dormitory.Student.Application.Catalog.StudentInfoRepository;
using Dormitory.Student.Application.Catalog.StudentInfoRepository.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dormitory.Student.Api.Controllers
{
    [Route("api/student")]
    //[Authorize]
    public class StudentInfoController : Controller
    {
        private readonly IStudentInfoRepo _studentInfoRepo;
        public StudentInfoController(IStudentInfoRepo studentInfoRepo)
        {
            _studentInfoRepo = studentInfoRepo;
        }
        [HttpGet("get-student-info")]
        public async Task<IActionResult> GetStudentByUserId([FromQuery] int userId)
        {
            var student = await _studentInfoRepo.GetStudentByUserId(userId);
            return Ok(student);
        }
        [HttpGet("get-room-price-range")]
        public async Task<IActionResult> GetRoomPriceRange()
        {
            var listRoomPrice = await _studentInfoRepo.GetRecomendRoomPrice();
            return Ok(listRoomPrice);
        }
        [HttpGet("get-list-student-confirm-contract")]
        public async Task<IActionResult> GetListStudentConfirmContract([FromQuery] GetListContractByStudentIdRepuest request)
        {
            var listContract = await _studentInfoRepo.GetListStudentConfirmContractPending(request);
            return Ok(listContract);
        }
        [HttpGet("check-sign-up-status")]
        public async Task<IActionResult> CheckSingnUpStatus([FromQuery] int studentId)
        {
            var check = await _studentInfoRepo.CheckCanSignUp(studentId);
            return Ok(check);
        }

        [HttpGet("check-create-extend-contract")]
        public async Task<IActionResult> CheckCreateExtendContract([FromQuery] int studentId)
        {
            var check = await _studentInfoRepo.CheckCanCreateExtendContract(studentId);
            return Ok(check);
        }
        [HttpGet("check-create-summer-contract")]
        public async Task<IActionResult> CheckCreateSummerContract([FromQuery] int studentId)
        {
            var check = await _studentInfoRepo.CheckCanCreateSummerContract(studentId);
            return Ok(check);
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateStudent([FromForm] StudentEntity student)
        {
            var result = await _studentInfoRepo.CreateStudent(student);
            if(result > 0)
            {
                return Ok(new { status = "success" });
            }
            else
            {
                return Ok(new { status = "error" });
            }
        }
    }
}
