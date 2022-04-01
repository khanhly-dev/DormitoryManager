using Dormitory.Admin.Application.Catalog.ServiceRepository.Dtos;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.ServiceRepository
{
    public interface IServiceRepo
    {
        Task<PageResult<ServiceDto>> GetList(PageRequestBase request);
        Task<int> CreateOrUpdate(ServiceEntity request);
        Task<int> Delete(int id);
    }
}
