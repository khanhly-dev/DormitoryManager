
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
        Task<PageResult<ContractDto>> GetListCompletedContract(PageRequestBase request);
        Task<PageResult<ContractPendingDto>> GetListContractPending(PageRequestBase request);
        Task<int> Create(CreateOrUpdateContractRequest request);
        Task<int> Delete(int id);
        Task<int> AdminConfirmContract(int contractId, int confirmStatus);
        Task<int> AdminConfirmAllContract();
        Task<PageResult<ContractPendingDto>> GetListAdminConfirmContractPending(PageRequestBase request);
        Task<int> ScheduleRoom(int contractId);
        Task<int> ChangeRoom(int contractId, int roomId);
        Task<int> UpdateRoomStatus(int contractId);
        Task<List<ContractFeeStatusDto>> GetListContractByStudentId(int studentId);

    }
}
