
using Dormitory.Admin.Application.Catalog.Dashboard;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dormitory.Admin.Api.Controllers
{
    [Route("api/dashboard")]
    //[Authorize]
    public class DashboardController : Controller
    {
        private readonly IDashboardRepo _dashboardRepo;
        public DashboardController(IDashboardRepo dashboardRepo)
        {
            _dashboardRepo = dashboardRepo;
        }
        [HttpGet("get-total-stat")]
        public async Task<IActionResult> GetListCriteria()
        {
            var list = await _dashboardRepo.GetBaseStat();
            return Ok(list);
        }
    }
}
