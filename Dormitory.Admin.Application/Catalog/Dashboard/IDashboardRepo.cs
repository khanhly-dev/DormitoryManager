using Dormitory.Admin.Application.Catalog.Dashboard.Dtos;
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
    }
}
