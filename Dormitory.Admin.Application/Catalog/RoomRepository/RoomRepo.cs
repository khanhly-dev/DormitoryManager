using Dormitory.Admin.Application.Catalog.RoomRepository.Dtos;
using Dormitory.Admin.Application.Catalog.RoomRepository.Requests;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
using Dormitory.EntityFrameworkCore.AdminEntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.RoomRepository
{
    public class RoomRepo : IRoomRepo
    {
        private readonly AdminSolutionDbContext _dbContext;
        public RoomRepo(AdminSolutionDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CreateOrUpdate(CreateOrUpdateRoomRequest request)
        {
            var room = new RoomEntity()
            {
                Id = request.Id,
                Name = request.Name,
                Price = request.Price,
                AreaId = request.AreaId,
                MaxSlot = request.MaxSlot,
                MinSlot = request.MinSlot,
                FilledSlot = request.FilledSlot.HasValue ? request.FilledSlot.Value : 0,
                EmptySlot = request.FilledSlot.HasValue ? request.MaxSlot - request.FilledSlot.Value : request.MaxSlot,
                AvaiableSlot = request.AvaiableSlot.HasValue ? request.AvaiableSlot.Value : 0
            };
            if (room.Id == 0)
            {
                _dbContext.RoomEntities.Add(room);
                return await _dbContext.SaveChangesAsync();
            }
            else if (room.Id > 0)
            {
                _dbContext.RoomEntities.Update(room);
                return await _dbContext.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> Delete(int id)
        {
            var room = await _dbContext.RoomEntities.FindAsync(id);
            if(room != null)
            {
                _dbContext.RoomEntities.Remove(room);
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<PageResult<RoomDto>> GetList(PageRequestBase request)
        {
            var query = from r in _dbContext.RoomEntities
                        join a in _dbContext.AreaEntities on r.AreaId equals a.Id
                        select new { a, r };

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.r.Name.Contains(request.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new RoomDto()
                {
                    Id = x.r.Id,
                    Name = x.r.Name,
                    Price = x.r.Price,
                    AreaId = x.r.AreaId,
                    AreaName = x.a.Name,
                    MaxSlot = x.r.MaxSlot,
                    FilledSlot = x.r.FilledSlot.Value,
                    MinSlot = x.r.MinSlot ?? 0,
                    AvaiableSlot = x.r.AvaiableSlot.Value,
                    EmptySlot = x.r.EmptySlot.Value,
                }).ToListAsync();

            foreach (var item in data)
            {
                var listRoomService = await _dbContext.RoomServiceEntities.Where(x => x.RoomId == item.Id).ToListAsync();
                var listRoomServiceFee = await _dbContext.RoomServiceFeeEntities.Where(t => listRoomService.Select(x => x.Id).ToList().Contains(t.RoomServiceId)).ToArrayAsync();
                if(listRoomServiceFee.Select(x => x.IsPaid).ToList().Contains(false))
                {
                    item.IsPaid = false;
                }
                else
                {
                    item.IsPaid = true;
                }
            }

            var pageResult = new PageResult<RoomDto>()
            {
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                TotalRecords = totalRow,
                Items = data
            };
            return pageResult;
        }

        public async Task<List<RoomSelectDto>> GetListEmptyRoom()
        {
            var listEmptyRoom = await _dbContext.RoomEntities.Where(x => x.AvaiableSlot > 0).Select(x => new RoomSelectDto
            {
                Id = x.Id,
                Name = x.Name,
                GenderRoom = x.RoomGender,
                AvaiableSlot = x.AvaiableSlot.Value
            }).ToListAsync();

            return listEmptyRoom;
        }
    }
}
