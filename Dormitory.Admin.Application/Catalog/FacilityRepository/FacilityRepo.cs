using Dormitory.Admin.Application.Catalog.FacilityRepository.Dtos;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
using Dormitory.EntityFrameworkCore.AdminEntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.FacilityRepository
{
    public class FacilityRepo : IFacilityRepo
    {
        private readonly AdminSolutionDbContext _dbContext;
        public FacilityRepo(AdminSolutionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddFacilityIntoRoom(FacilityInRoomEntity request)
        {
            var facility = await _dbContext.FacilityInRoomEntities.Where(x => x.FacilityId == request.FacilityId && x.RoomId == request.RoomId).FirstOrDefaultAsync();
            if(facility != null)
            {
                facility.Count += request.Count;
                facility.Status = request.Status;
            }
            else
            {
                _dbContext.FacilityInRoomEntities.Add(request);
            }    
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> CreateOrUpdate(FacilityEntity request)
        {
            var facility = new FacilityEntity()
            {
                Id = request.Id,
                Name = request.Name,
                TotalCount = request.TotalCount,
            };
            if (facility.Id == 0)
            {
                _dbContext.FacilityEntities.Add(facility);
                return await _dbContext.SaveChangesAsync();
            }
            else if (facility.Id > 0)
            {
                _dbContext.FacilityEntities.Update(facility);
                return await _dbContext.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> Delete(int id)
        {
            var facility = await _dbContext.FacilityEntities.FindAsync(id);
            if (facility != null)
            {
                _dbContext.FacilityEntities.Remove(facility);
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteFacilityInRoom(int id)
        {
            var facilityInRoom = await _dbContext.FacilityInRoomEntities.FindAsync(id);
            if(facilityInRoom != null)
            {
                _dbContext.FacilityInRoomEntities.Remove(facilityInRoom);
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<FacilityInRoomDto>> GetFacilityByRoomId(int roomId)
        {
            var query = from f in _dbContext.FacilityInRoomEntities
                        join fa in _dbContext.FacilityEntities on f.FacilityId equals fa.Id
                        where f.RoomId == roomId
                        select new { f, fa };
            var list = await query.Select(x => new FacilityInRoomDto
            {
                Id = x.f.Id,
                FacilityId = x.f.FacilityId,
                FacilityName = x.fa.Name,
                Count = x.f.Count,
                RoomId = x.f.RoomId,
                Status = x.f.Status,
            }).ToListAsync();
            return list;
        }

        public async Task<PageResult<FacilityDto>> GetList(PageRequestBase request)
        {
            var query = from a in _dbContext.FacilityEntities
                        select a;

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Name.Contains(request.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new FacilityDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    TotalCount = x.TotalCount,
                }).ToListAsync();

            var pageResult = new PageResult<FacilityDto>()
            {
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                TotalRecords = totalRow,
                Items = data
            };
            return pageResult;
        }

        public async Task<List<FacilityInRoomDto>> GetListFacilityInRoom()
        {
            var query = from f in _dbContext.FacilityInRoomEntities
                        join fa in _dbContext.FacilityEntities on f.FacilityId equals fa.Id
                        select new { f, fa };
            var list = await query.Select(x => new FacilityInRoomDto
            {
                Id = x.f.Id,
                FacilityId = x.f.FacilityId,
                FacilityName = x.fa.Name,
                Count = x.f.Count,
                RoomId = x.f.RoomId,
                Status = x.f.Status,
            }).ToListAsync(); 
            return list;
        }

        public async Task<List<ComboSelectDto>> GetListSelect()
        {
            var list = await _dbContext.FacilityEntities.Select(x => new ComboSelectDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            return list;
        }
    }
}
