using Dormitory.Admin.Application.Catalog.FacilityRepository.Dtos;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.FacilityRepository
{
    public interface IFacilityRepo
    {
        public Task<PageResult<FacilityDto>> GetList(PageRequestBase request);
        public Task<int> CreateOrUpdate(FacilityEntity request);
        public Task<int> Delete(int id);
    }
}
