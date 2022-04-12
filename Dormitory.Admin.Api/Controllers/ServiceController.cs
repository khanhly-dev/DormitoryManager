using Dormitory.Admin.Application.Catalog.ServiceRepository;
using Dormitory.Admin.Application.Catalog.ServiceRepository.Requests;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dormitory.Admin.Api.Controllers
{
    [Route("api/service")]
    //[Authorize]
    public class ServiceController : Controller
    {
        private readonly IServiceRepo _serviceRepo;
        public ServiceController(IServiceRepo serviceRepo)
        {
            _serviceRepo = serviceRepo;
        }
        [HttpGet("get-list")]
        public async Task<IActionResult> GetListService([FromQuery] PageRequestBase request)
        {
            var list = await _serviceRepo.GetList(request);
            return Ok(list);
        }
        [HttpGet("get-list-select")]
        public async Task<IActionResult> GetListServiceSelect()
        {
            var list = await _serviceRepo.GetListSelect();
            return Ok(list);
        }
        [HttpGet("get-service-in-room")]
        public async Task<IActionResult> GetListServiceInRoom([FromQuery] int roomId)
        {
            var list = await _serviceRepo.GetServiceByRoom(roomId);
            return Ok(list);
        }
        [HttpGet("get-service-in-bill")]
        public async Task<IActionResult> GetListServiceInBill([FromQuery] int billId)
        {
            var list = await _serviceRepo.GetServiceByBill(billId);
            return Ok(list);
        }
        [HttpPost("create-or-update")]
        public async Task<IActionResult> CreateOrUpdateService([FromForm] ServiceEntity request)
        {
            var responseStatus = "";
            var result = await _serviceRepo.CreateOrUpdate(request);
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
        [HttpPost("add-service-for-room")]
        public async Task<IActionResult> AddServiceForRoom([FromForm] string request, [FromForm] int roomId, [FromForm] DateTime fromDate, [FromForm] DateTime toDate)
        {
            var responseStatus = "";
            var requestConvert = JsonConvert.DeserializeObject<List<AddServiceForRoomRequest>>(request);
            var result = await _serviceRepo.AddServiceForRoom(requestConvert, roomId, fromDate, toDate);
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
        public async Task<IActionResult> DeleteService([FromQuery] int id)
        {
            var responseStatus = "";
            var result = await _serviceRepo.Delete(id);
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
        [HttpDelete("delete-bill-service")]
        public async Task<IActionResult> DeleteBillService([FromQuery] int billId)
        {
            var responseStatus = "";
            var result = await _serviceRepo.DeleteBillService(billId);
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
        [HttpDelete("delete-room-service")]
        public async Task<IActionResult> DeleteRoomService([FromQuery] int roomServiceId)
        {
            var responseStatus = "";
            var result = await _serviceRepo.DeleteRoomServiceFee(roomServiceId);
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
        [HttpPut("update-room-service-fee")]
        public async Task<IActionResult> UpdateRoomServiceFee([FromForm] int RoomServiceId, [FromForm] float moneyPaid, [FromForm] DateTime datePaid)
        {
            var responseStatus = "";
            var result = await _serviceRepo.UpdateRoomServiceFee(RoomServiceId, moneyPaid, datePaid);
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
