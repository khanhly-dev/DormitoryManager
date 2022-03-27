using Dormitory.Admin.Application.Catalog.AreaRepository.Dtos;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
using Dormitory.EntityFrameworkCore.AdminEntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.AreaRepository
{
    public class AreaRepo : IAreaRepo
    {
        private readonly AdminSolutionDbContext _dbContext;
        public AreaRepo(AdminSolutionDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CreateOrUpdate(AreaEntity request)
        {
            
            var area = new AreaEntity()
            {
                Id = request.Id,
                Name = request.Name,
                TotalRoom = request.TotalRoom,
            };
            if(area.Id == 0)
            {
                _dbContext.AreaEntities.Add(area);
                return await _dbContext.SaveChangesAsync();
            }
            else if(area.Id > 0)
            {
                _dbContext.AreaEntities.Update(area);
                return await _dbContext.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> Delete(int id)
        {
            var area = await _dbContext.AreaEntities.FindAsync(id);
            if(area != null)
            {
                _dbContext.AreaEntities.Remove(area);
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<PageResult<AreaDto>> GetList(PageRequestBase request)
        {
            var query = from a in _dbContext.AreaEntities
                        select a;

            if(!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Name.Contains(request.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new AreaDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    TotalRoom = x.TotalRoom ?? 0,
                }).ToListAsync();

            var pageResult = new PageResult<AreaDto>()
            {
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                TotalRecords = totalRow,
                Items = data
            };
            return pageResult;
        }

        public async Task<List<AreaSelectDto>> GetListAreaSelect()
        {
            var query = from a in _dbContext.AreaEntities
                        select a;

            var data = await query.Select(x => new AreaSelectDto
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();

            return data;
        }
    }
}
