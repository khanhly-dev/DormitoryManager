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
        Task<PageResult<FacilityDto>> GetList(PageRequestBase request);
        Task<int> CreateOrUpdate(FacilityEntity request);
        Task<int> Delete(int id);

        Task<List<FacilityInRoomDto>> GetListFacilityInRoom();
        Task<int> AddFacilityIntoRoom(FacilityInRoomEntity request);
        Task<int> DeleteFacilityInRoom(int id);
        Task<List<FacilityInRoomDto>> GetFacilityByRoomId(int roomId);
        Task<List<ComboSelectDto>> GetListSelect();
    }
}
