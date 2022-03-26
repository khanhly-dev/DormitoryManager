using Dormitory.Admin.Application.Catalog.CriteriaRepository;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
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
        public async Task<IActionResult> GetListCriteria(PageRequestBase request)
        {
            var list = await _criteriaRepo.GetList(request);
            return Ok(list);
        }
        [HttpPost("create-or-update")]
        public async Task<IActionResult> CreateOrUpdateCriteria(CriteriaConfigEntity request)
        {
            var responseStatus = "";
            var result = await _criteriaRepo.CreateOrUpdate(request);
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
        public async Task<IActionResult> DeleteCriteria(int id)
        {
            var responseStatus = "";
            var result = await _criteriaRepo.Delete(id);
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
