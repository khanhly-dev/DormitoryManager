using Dormitory.Admin.Application.Catalog.RoomRepository.Dtos;
using Dormitory.Admin.Application.CommonDto;
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
        public Task<PageResult<RoomDto>> GetList(PageRequestBase request);
        public Task<int> CreateOrUpdate(RoomEntity request);
        public Task<int> Delete(int id);
    }
}
