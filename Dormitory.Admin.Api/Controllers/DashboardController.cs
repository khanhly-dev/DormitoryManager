
using Dormitory.Admin.Application.Catalog.Dashboard;
using Dormitory.Admin.Application.CommonDto;
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
        public async Task<IActionResult> GetTotalStat()
        {
            var list = await _dashboardRepo.GetBaseStat();
            return Ok(list);
        }
        [HttpGet("get-list-contract-fee")]
        public async Task<IActionResult> GetListContractFee(PageRequestBase request)
        {
            var list = await _dashboardRepo.GetContractFee(request);
            return Ok(list);
        }
        [HttpGet("get-list-service-fee")]
        public async Task<IActionResult> GetListServiceFee(PageRequestBase request)
        {
            var list = await _dashboardRepo.GetServiceFee(request);
            return Ok(list);
        }
        [HttpGet("get-gender-percent")]
        public async Task<IActionResult> GetGenderPercent()
        {
            var list = await _dashboardRepo.getGenderPercent();
            return Ok(list);
        }
        [HttpGet("get-contract-fee-chart")]
        public async Task<IActionResult> GetContractFeeChart()
        {
            var list = await _dashboardRepo.GetContractFeeChart();
            return Ok(list);
        }
        [HttpGet("get-service-fee-chart")]
        public async Task<IActionResult> GetServiceFeeChart()
        {
            var list = await _dashboardRepo.GetServiceFeeChart();
            return Ok(list);
        }
        [HttpGet("get-area-chart")]
        public async Task<IActionResult> GetAreaChart()
        {
            var list = await _dashboardRepo.GetAreaChart();
            return Ok(list);
        }
    }
}
