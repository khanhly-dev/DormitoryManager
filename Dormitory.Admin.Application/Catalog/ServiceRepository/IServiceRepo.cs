using Dormitory.Admin.Application.Catalog.ServiceRepository.Dtos;
using Dormitory.Admin.Application.Catalog.ServiceRepository.Requests;
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
        Task<int> AddServiceForRoom(List<AddServiceForRoomRequest> request, int roomId, DateTime fromDate, DateTime toDate);
        Task<List<RoomServiceDto>> GetServiceByRoom(int roomId);
        Task<List<ComboSelectDto>> GetListSelect();
        Task<int> UpdateRoomServiceFee(int roomServiceId, float moneyPaid, DateTime datePaid);
        Task<int> DeleteRoomServiceFee(int roomServiceId);
        Task<List<RoomServiceDto>> GetServiceByBill(int billId);
        Task<int> DeleteBillService(int billId);
    }
}
