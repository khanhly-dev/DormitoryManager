using Dormitory.Admin.Application.Catalog.ContractTimeConfigRepository.Dtos;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntites;
using Dormitory.EntityFrameworkCore.AdminEntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.ContractTimeConfigRepository
{
    public class ContractTimeConfigRepo : IContractTimeConfigRepo
    {
        private readonly AdminSolutionDbContext _dbContext;
        public ContractTimeConfigRepo(AdminSolutionDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CreateOrUpdate(ContractTimeConfigEntity request)
        {
            var config = new ContractTimeConfigEntity()
            {
                Id = request.Id,
                Name = request.Name,
                FromDate = request.FromDate,
                ToDate = request.ToDate,
                IsSummerSemester = request.IsSummerSemester,
            };
            if (config.Id == 0)
            {
                _dbContext.ContractTimeConfigEntities.Add(config);
                return await _dbContext.SaveChangesAsync();
            }
            else if (config.Id > 0)
            {
                _dbContext.ContractTimeConfigEntities.Update(config);
                return await _dbContext.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> Delete(int id)
        {
            var config = await _dbContext.ContractTimeConfigEntities.FindAsync(id);
            if (config != null)
            {
                _dbContext.ContractTimeConfigEntities.Remove(config);
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<PageResult<ContractTimeConfigDto>> GetList(PageRequestBase request)
        {
            var query = from a in _dbContext.ContractTimeConfigEntities
                        select a;

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Name.Contains(request.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ContractTimeConfigDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    FromDate = x.FromDate,
                    ToDate = x.ToDate,
                    IsSummerSemester = x.IsSummerSemester,
                }).ToListAsync();

            var pageResult = new PageResult<ContractTimeConfigDto>()
            {
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                TotalRecords = totalRow,
                Items = data
            };
            return pageResult;
        }

        public async Task<List<ContractConfigSelectDto>> GetListSelect()
        {
            var data = await _dbContext.ContractTimeConfigEntities.Select(x => new ContractConfigSelectDto
            {
                Id = x.Id,
                Name = x.Name,
                FromDate = x.FromDate,
                ToDate = x.ToDate
            }).ToListAsync();

            return data;
        }
    }
}
