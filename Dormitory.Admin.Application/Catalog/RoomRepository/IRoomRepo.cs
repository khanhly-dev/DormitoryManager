using Dormitory.Admin.Application.Catalog.RoomRepository.Dtos;
using Dormitory.Admin.Application.Catalog.RoomRepository.Requests;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntites;
using Dormitory.Domain.AppEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.RoomRepository
{
    public interface IRoomRepo
    {
        Task<PageResult<RoomDto>> GetList(PageRequestBase request);
        Task<int> CreateOrUpdate(CreateOrUpdateRoomRequest request);
        Task<int> Delete(int id);
        Task<List<RoomSelectDto>> GetListEmptyRoom();
        Task<List<ComboSelectDto>> GetListRoomSelect();
        Task<List<BillServiceDto>> GetBillByRoom(int roomId);
    }
}
