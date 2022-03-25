using Dormitory.Admin.Application.Catalog.CriteriaRepository.Dtos;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
using Dormitory.EntityFrameworkCore.AdminEntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.CriteriaRepository
{
    public class CriteriaRepo : ICriteriaRepo
    {
        private readonly AdminSolutionDbContext _dbContext;
        public CriteriaRepo(AdminSolutionDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CreateOrUpdate(CriteriaConfigEntity request)
        {
            var criterial = new CriteriaConfigEntity()
            {
                Id = request.Id,
                Name = request.Name,
                Point = request.Point,  
            };
            if (criterial.Id == 0)
            {
                _dbContext.CriteriaConfigEntities.Add(criterial);
                return await _dbContext.SaveChangesAsync();
            }
            else if (criterial.Id > 0)
            {
                _dbContext.CriteriaConfigEntities.Update(criterial);
                return await _dbContext.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> Delete(int id)
        {
            var criterial = await _dbContext.CriteriaConfigEntities.FindAsync(id);
            if (criterial != null)
            {
                _dbContext.CriteriaConfigEntities.Remove(criterial);
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<PageResult<CriteriaDto>> GetList(PageRequestBase request)
        {
            var query = from a in _dbContext.CriteriaConfigEntities
                        select a;

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Name.Contains(request.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new CriteriaDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Point = x.Point,
                }).ToListAsync();

            var pageResult = new PageResult<CriteriaDto>()
            {
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                TotalRecords = totalRow,
                Items = data
            };
            return pageResult;
        }
    }
}
