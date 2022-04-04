using Dormitory.Student.Application.Catalog.StudentInfoRepository;
using Dormitory.Student.Application.Catalog.StudentInfoRepository.Dtos;
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
            var listRoomPrice = await _studentInfoRepo.GetListStudentConfirmContractPending(request);
            return Ok(listRoomPrice);
        }
    }
}
