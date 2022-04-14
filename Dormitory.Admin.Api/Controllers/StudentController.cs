using Dormitory.Admin.Application.Catalog.StudentRepository;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntites;
using Dormitory.Domain.AppEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Dormitory.Admin.Api.Controllers
{
    [Route("api/student")]
    //[Authorize]
    public class StudentController : Controller
    {
        private readonly IStudentRepo _studentRepo;
        public StudentController(IStudentRepo studentRepo)
        {
            _studentRepo = studentRepo;
        }
        [HttpGet("get-list")]
        public async Task<IActionResult> GetListStudent([FromQuery] PageRequestBase request)
        {
            var list = await _studentRepo.GetList(request);
            return Ok(list);
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllStudent([FromQuery] PageRequestBase request)
        {
            var list = await _studentRepo.GetAll(request);
            return Ok(list);
        }
        [HttpGet("get-list-student-select")]
        public async Task<IActionResult> GetListStudentSelect()
        {
            var list = await _studentRepo.GetListStudentSelect();
            return Ok(list);
        }
        [HttpGet("get-list-discipline")]
        public async Task<IActionResult> GetListDiscipline([FromQuery] PageRequestBase request)
        {
            var list = await _studentRepo.GetListDiscipline(request);
            return Ok(list);
        }
        [HttpPost("create-or-update")]
        public async Task<IActionResult> CreateOrUpdateStudent([FromForm] StudentEntity request)
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
            return Ok(new { responseStatus });
        }
        [HttpPut("update-contract-fee")]
        public async Task<IActionResult> UpdateContractFee(int contractId, float moneyPaid, DateTime datePaid)
        {
            var responseStatus = "";
            var result = await _studentRepo.UpdateContractFee(contractId, datePaid, moneyPaid);
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
            return Ok(new { responseStatus });
        }

        [HttpPost("add-or-update-discipline")]
        public async Task<IActionResult> AddDiscipline([FromForm] DisciplineEntity request)
        {
            var responseStatus = "";
            var result = await _studentRepo.AddDiscipline(request);
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
        [HttpPut("update-discipline")]
        public async Task<IActionResult> UpdateDiscipline([FromForm] DisciplineEntity request)
        {
            var responseStatus = "";
            var result = await _studentRepo.UpdateDiscipline(request);
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
        [HttpDelete("delete-discipline")]
        public async Task<IActionResult> DeleteDiscipline([FromQuery] int id)
        {
            var responseStatus = "";
            var result = await _studentRepo.DeleteDiscipline(id);
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
    }
}
