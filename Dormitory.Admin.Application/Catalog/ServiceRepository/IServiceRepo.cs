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
        public Task<PageResult<ServiceDto>> GetList(PageRequestBase request);
        public Task<int> CreateOrUpdate(ServiceEntity request);
        public Task<int> Delete(int id);
    }
}
