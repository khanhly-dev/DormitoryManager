﻿using Dormitory.Admin.Application.Catalog.ContractTimeConfigRepository;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntites;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dormitory.Admin.Api.Controllers
{
    [Route("api/contract-time-config")]
    //[Authorize]
    public class ContractTimeConfigController : Controller
    {
        private readonly IContractTimeConfigRepo _contractTimeConfigRepo;
        public ContractTimeConfigController(IContractTimeConfigRepo contractTimeConfigRepo)
        {
            _contractTimeConfigRepo = contractTimeConfigRepo;
        }
        [HttpGet("get-list")]
        public async Task<IActionResult> GetListContractConfig(PageRequestBase request)
        {
            var listArea = await _contractTimeConfigRepo.GetList(request);
            return Ok(listArea);
        }
        [HttpPost("create-or-update")]
        public async Task<IActionResult> CreateOrUpdateContractConfig(ContractTimeConfigEntity request)
        {
            var responseStatus = "";
            var result = await _contractTimeConfigRepo.CreateOrUpdate(request);
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
        public async Task<IActionResult> DeleteContractConfig(int id)
        {
            var responseStatus = "";
            var result = await _contractTimeConfigRepo.Delete(id);
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
