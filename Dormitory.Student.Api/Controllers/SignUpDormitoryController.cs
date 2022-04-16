using Dormitory.Student.Application.Catalog.SignUpDormitory;
using Dormitory.Student.Application.Catalog.SignUpDormitory.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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
        [HttpPost("extend-contract")]
        public async Task<IActionResult> CreateExtendContract([FromForm] int studentId)
        {
            var responseStatus = "";
            var result = await _signUpDormitoryRepo.CreateExtendContract(studentId);
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
        [HttpPost("summer-contract")]
        public async Task<IActionResult> CreateSummerContract([FromForm] int studentId)
        {
            var responseStatus = "";
            var result = await _signUpDormitoryRepo.CreateSummerSemesterContract(studentId);
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
        [HttpPost("set-student-point")]
        public async Task<IActionResult> SetStudentPoint([FromForm] int studentId, [FromForm] string listCriteriaId)
        {
            var responseStatus = "";
            var listIdResult = listCriteriaId.Split(',').Select(x => Int32.Parse(x)).ToList();
            var request = new SetStudentPointRepuest();
            request.StudentId = studentId;
            request.ListCriteriaId = listIdResult;
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
        [HttpGet("get-extend-contract-time")]
        public async Task<IActionResult> GetExterndContractTime([FromQuery] int studentId)
        {
            var extendContractTime = await _signUpDormitoryRepo.GetExtendContractTime(studentId);
            return Ok(extendContractTime);
        }
        [HttpGet("get-summer-contract-time")]
        public async Task<IActionResult> GetSummerContractTime([FromQuery] int studentId)
        {
            var contractTime = await _signUpDormitoryRepo.GetSummerContractTime(studentId);
            return Ok(contractTime);
        }
        [HttpPut("student-confirm")]
        public async Task<IActionResult> StudentConfirm([FromForm] int contractId, [FromForm] int confirmStatus)
        {
            var responseStatus = "";
            var result = await _signUpDormitoryRepo.StudentConfirmContract(contractId, confirmStatus);
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
        public async Task<IActionResult> DeleteContract([FromQuery] int id)
        {
            var responseStatus = "";
            var result = await _signUpDormitoryRepo.Delete(id);
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
