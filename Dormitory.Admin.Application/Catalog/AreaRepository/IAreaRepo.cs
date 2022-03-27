using Dormitory.Admin.Application.Catalog.AreaRepository.Dtos;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.AreaRepository
{
    public interface IAreaRepo
    {
        public Task<PageResult<AreaDto>> GetList(PageRequestBase request);
        public Task<int> CreateOrUpdate(AreaEntity request);
        public Task<int> Delete(int id);
        public Task<List<AreaSelectDto>> GetListAreaSelect();
    }
}
