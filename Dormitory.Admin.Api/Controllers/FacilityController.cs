﻿using Dormitory.Admin.Application.Catalog.FacilityRepository;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dormitory.Admin.Api.Controllers
{
    [Route("api/facility")]
    //[Authorize]
    public class FacilityController : Controller
    {
        private readonly IFacilityRepo _facilityRepo;
        public FacilityController(IFacilityRepo facilityRepo)
        {
            _facilityRepo = facilityRepo;
        }
        [HttpGet("get-list")]
        public async Task<IActionResult> GetListFacility(PageRequestBase request)
        {
            var list = await _facilityRepo.GetList(request);
            return Ok(list);
        }
        [HttpPost("create-or-update")]
        public async Task<IActionResult> CreateOrUpdateFacility(FacilityEntity request)
        {
            var responseStatus = "";
            var result = await _facilityRepo.CreateOrUpdate(request);
            if (result == 1)
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
        public async Task<IActionResult> DeleteFacility(int id)
        {
            var responseStatus = "";
            var result = await _facilityRepo.Delete(id);
            if (result == 1)
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
