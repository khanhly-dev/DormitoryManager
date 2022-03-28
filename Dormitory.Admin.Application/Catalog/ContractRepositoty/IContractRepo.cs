
using Dormitory.Admin.Application.Catalog.ContractRepositoty.Dtos;
using Dormitory.Admin.Application.Catalog.ContractRepositoty.Requests;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.ContractRepositoty
{
    public interface IContractRepo
    {
        public Task<PageResult<ContractDto>> GetList(PageRequestBase request);
        public Task<PageResult<ContractPendingDto>> GetListContractPending(PageRequestBase request);
        public Task<int> Create(CreateOrUpdateContractRequest request);
        public Task<int> Delete(int id);
    }
}
