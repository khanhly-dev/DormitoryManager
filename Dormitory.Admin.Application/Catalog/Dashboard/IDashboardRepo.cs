using Dormitory.Admin.Application.Catalog.Dashboard.Dtos;
using Dormitory.Admin.Application.CommonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.Dashboard
{
    public interface IDashboardRepo
    {
        Task<BaseStatDto> GetBaseStat();
        Task<PageResult<FeeDto>> GetContractFee(PageRequestBase request);
        Task<PageResult<FeeDto>> GetServiceFee(PageRequestBase request);
        Task<GenderPercentDto> getGenderPercent();
        Task<FeeChart> GetContractFeeChart();
        Task<FeeChart> GetServiceFeeChart();
        Task<List<AreaChart>> GetAreaChart();
    }
}
