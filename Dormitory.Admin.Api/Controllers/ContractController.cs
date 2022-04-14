using Dormitory.Admin.Application.Catalog.ContractRepositoty;
using Dormitory.Admin.Application.Catalog.ContractRepositoty.Requests;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dormitory.Admin.Api.Controllers
{
    [Route("api/contract")]
    //[Authorize]
    public class ContractController : Controller
    {
        private readonly IContractRepo _contractRepo;
        public ContractController(IContractRepo contractRepo)
        {
            _contractRepo = contractRepo;
        }
        [HttpGet("get-list-completed-contract")]
        public async Task<IActionResult> GetListCompletedContract([FromQuery] PageRequestBase request)
        {
            var list = await _contractRepo.GetListCompletedContract(request);
            return Ok(list);
        }
        [HttpGet("get-list-contract-by-student")]
        public async Task<IActionResult> GetListContractByStudentId([FromQuery] int studentId)
        {
            var list = await _contractRepo.GetListContractByStudentId(studentId);
            return Ok(list);
        }
        [HttpGet("get-list-contract-pending")]
        public async Task<IActionResult> GetListContractPeding([FromQuery] PageRequestBase request)
        {
            var list = await _contractRepo.GetListContractPending(request);
            return Ok(list);
        }
        [HttpGet("get-list-admin-confirm-contract-pending")]
        public async Task<IActionResult> GetListAdminConfirmContractPeding([FromQuery] PageRequestBase request)
        {
            var list = await _contractRepo.GetListAdminConfirmContractPending(request);
            return Ok(list);
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateContract([FromForm] CreateOrUpdateContractRequest request)
        {
            var responseStatus = "";
            var result = await _contractRepo.Create(request);
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
            var result = await _contractRepo.Delete(id);
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
        [HttpPost("admin-confirm")]
        public async Task<IActionResult> AdminConfirmContract([FromForm]int contractId, [FromForm]int confirmStatus)
        {
            var responseStatus = "";
            var result = await _contractRepo.AdminConfirmContract(contractId, confirmStatus);
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
        [HttpPost("admin-all-confirm")]
        public async Task<IActionResult> AdminConfirmAllContract()
        {
            var responseStatus = "";
            var result = await _contractRepo.AdminConfirmAllContract();
            if (result > 0)
            {
                responseStatus = "success";
            }
            else
            {
                responseStatus = "error";
            }
            return Ok(new { status = responseStatus, count = result });
        }
        [HttpPut("schedule-room")]
        public async Task<IActionResult> ScheduleRoom([FromForm] int contractId)
        {
            var responseStatus = "";
            var result = await _contractRepo.ScheduleRoom(contractId);
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
        [HttpPut("auto-schedule-room")]
        public async Task<IActionResult> AutoScheduleRoom([FromForm] string listContractId)
        {
            var responseStatus = "";
            var listIdResult = listContractId.Split(',').Select(x => Int32.Parse(x)).ToList();
            var result = 0;
            foreach (var item in listIdResult)
            {
                result = await _contractRepo.ScheduleRoom(item);
            }
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
        [HttpPut("change-room")]
        public async Task<IActionResult> ChangeRoom([FromForm] int contractId, [FromForm] int roomId)
        {
            var responseStatus = "";
            var result = await _contractRepo.ChangeRoom(contractId, roomId);
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
