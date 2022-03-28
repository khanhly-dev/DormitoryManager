using Dormitory.Admin.Application.Catalog.ContractRepositoty.Dtos;
using Dormitory.Admin.Application.Catalog.ContractRepositoty.Requests;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
using Dormitory.Domain.Shared.Constant;
using Dormitory.EntityFrameworkCore.AdminEntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.ContractRepositoty
{
    public class ContractRepo : IContractRepo
    {
        private readonly AdminSolutionDbContext _dbContext;
        public ContractRepo(AdminSolutionDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Create(CreateOrUpdateContractRequest request)
        {
            var criterial = new ContractEntity()
            {
                ContractCode = request.ContractCode,
                DateCreated = DateTime.Now,
                FromDate = request.FromDate,
                ToDate = request.ToDate,  
                RoomId = request.RoomId,
                DesiredRoomId = request.DesiredRoomId,
                StudentId = request.StudentId,
                ServiceId = request.ServiceId,
                AdminConfirmStatus = request.AdminConfirmStatus.HasValue ? request.AdminConfirmStatus.Value : DataConfigConstant.adminConfirmStatusFalse,
                StudentConfirmStatus = request.StudentConfirmStatus.HasValue ? request.StudentConfirmStatus : DataConfigConstant.studetnConfirmStatusFalse,
            };
            
            _dbContext.ContractEntities.Add(criterial);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var contract = await _dbContext.ContractEntities.FindAsync(id);
            if (contract != null)
            {
                _dbContext.ContractEntities.Remove(contract);
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<PageResult<ContractDto>> GetList(PageRequestBase request)
        {
            var query = from a in _dbContext.ContractEntities
                        select a;

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.ContractCode.Contains(request.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ContractDto()
                {
                    Id = x.Id,
                    ContractCode = x.ContractCode,
                    FromDate = x.FromDate,
                    ToDate = x.ToDate,
                    RoomId = x.RoomId,
                    DesiredRoomId = x.DesiredRoomId,
                    StudentId = x.StudentId,
                    ServiceId = x.ServiceId,
                    AdminConfirmStatus = x.AdminConfirmStatus,
                    StudentConfirmStatus = x.StudentConfirmStatus,
                }).ToListAsync();

            var pageResult = new PageResult<ContractDto>()
            {
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                TotalRecords = totalRow,
                Items = data
            };
            return pageResult;
        }

        public async Task<PageResult<ContractPendingDto>> GetListContractPending(PageRequestBase request)
        {
            var query = from a in _dbContext.ContractEntities
                        join s in _dbContext.StudentEntities on a.StudentId equals s.Id
                        join r in _dbContext.RoomEntities on a.DesiredRoomId equals r.Id into ra
                        from r in ra.DefaultIfEmpty()
                        where a.AdminConfirmStatus == DataConfigConstant.adminConfirmStatusFalse
                        select new { a, s, r};

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.a.ContractCode.Contains(request.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ContractPendingDto()
                {
                    Id = x.a.Id,
                    ContractCode = x.a.ContractCode,
                    DateCreated = x.a.DateCreated,
                    DesiredRoomId = x.a.DesiredRoomId,
                    DesiredRoomName = x.r == null ? null : x.r.Name,
                    StudentId = x.a.StudentId,
                    StudentName = x.s.Name,
                    StudentCode = x.s.StudentCode,
                    StudentPhone = x.s.Phone,
                    Gender = x.s.Gender,
                    Adress = x.s.Adress,
                    AdminConfirmStatus = x.a.AdminConfirmStatus,
                    Point = x.s.Point
                }).ToListAsync();

            var pageResult = new PageResult<ContractPendingDto>()
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
