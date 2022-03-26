using Dormitory.Admin.Application.Catalog.ContractTimeConfigRepository.Dtos;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.ContractTimeConfigRepository
{
    public interface IContractTimeConfigRepo
    {
        public Task<PageResult<ContractTimeConfigDto>> GetList(PageRequestBase request);
        public Task<int> CreateOrUpdate(ContractTimeConfigEntity request);
        public Task<int> Delete(int id);
    }
}
