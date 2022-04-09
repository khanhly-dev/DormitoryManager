using Dormitory.Admin.Application.Catalog.ServiceRepository.Dtos;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
using Dormitory.EntityFrameworkCore.AdminEntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.ServiceRepository
{
    public class ServiceRepo : IServiceRepo
    {
        private readonly AdminSolutionDbContext _dbContext;
        public ServiceRepo(AdminSolutionDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CreateOrUpdate(ServiceEntity request)
        {
            var service = new ServiceEntity()
            {
                Id = request.Id,
                Name = request.Name,
                Price = request.Price,
                ServiceType = request.ServiceType,
                Unit = request.Unit,
            };
            if (service.Id == 0)
            {
                _dbContext.ServiceEntities.Add(service);
                return await _dbContext.SaveChangesAsync();
            }
            else if (service.Id > 0)
            {
                _dbContext.ServiceEntities.Update(service);
                return await _dbContext.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> Delete(int id)
        {
            var service = await _dbContext.ServiceEntities.FindAsync(id);
            if (service != null)
            {
                _dbContext.ServiceEntities.Remove(service);
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<PageResult<ServiceDto>> GetList(PageRequestBase request)
        {
            var query = from a in _dbContext.ServiceEntities
                        select a;

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Name.Contains(request.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ServiceDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Unit = x.Unit,
                    ServiceType = x.ServiceType
                }).ToListAsync();

            var pageResult = new PageResult<ServiceDto>()
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
