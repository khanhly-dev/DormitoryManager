using Dormitory.Admin.Application.Catalog.CriteriaRepository;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dormitory.Admin.Api.Controllers
{
    [Route("api/criteria")]
    //[Authorize]
    public class CriteriaController : Controller
    {
        private readonly ICriteriaRepo _criteriaRepo;
        public CriteriaController(ICriteriaRepo criteriaRepo)
        {
            _criteriaRepo = criteriaRepo;
        }
        [HttpGet("get-list")]
        public async Task<IActionResult> GetListCriteria([FromQuery] PageRequestBase request)
        {
            var list = await _criteriaRepo.GetList(request);
            return Ok(list);
        }
        [HttpPost("create-or-update")]
        public async Task<IActionResult> CreateOrUpdateCriteria([FromForm] CriteriaConfigEntity request)
        {
            var responseStatus = "";
            var result = await _criteriaRepo.CreateOrUpdate(request);
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
        public async Task<IActionResult> DeleteCriteria([FromQuery] int id)
        {
            var responseStatus = "";
            var result = await _criteriaRepo.Delete(id);
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
