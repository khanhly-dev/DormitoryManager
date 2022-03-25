using Dormitory.Admin.Application.Catalog.CriteriaRepository.Dtos;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.CriteriaRepository
{
    public interface ICriteriaRepo
    {
        public Task<PageResult<CriteriaDto>> GetList(PageRequestBase request);
        public Task<int> CreateOrUpdate(CriteriaConfigEntity request);
        public Task<int> Delete(int id);
    }
}
