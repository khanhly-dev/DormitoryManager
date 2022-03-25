﻿using Dormitory.Admin.Application.Catalog.AreaRepository;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dormitory.Admin.Api.Controllers
{
    [Route("api/area")]
    //[Authorize]
    public class AreaController : Controller
    {
        private readonly IAreaRepo _areaRepo;
        public AreaController(IAreaRepo areaRepo)
        {
            _areaRepo = areaRepo;
        }
        [HttpGet("get-list")]
        public async Task<IActionResult> GetListArea(PageRequestBase request)
        {
            var listArea = await _areaRepo.GetList(request);
            return Ok(listArea);
        }
        [HttpPost("create-or-update")]
        public async Task<IActionResult> CreateOrUpdateArea(AreaEntity request)
        {
            var responseStatus = "";
            var result = await _areaRepo.CreateOrUpdate(request);
            if(result == 1)
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
        public async Task<IActionResult> DeleteArea(int id)
        {
            var responseStatus = "";
            var result = await _areaRepo.Delete(id);
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
