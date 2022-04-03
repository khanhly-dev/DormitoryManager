﻿using Dormitory.Admin.Application.Catalog.ContractRepositoty.Dtos;
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

        public async Task<int> AdminConfirmAllContract(int minPoint, int maxPoint, int confirmStatus)
        {
            var query = from a in _dbContext.ContractEntities
                        join s in _dbContext.StudentEntities on a.StudentId equals s.Id
                        where s.Point >= minPoint && s.Point < maxPoint
                        select new { a, s };

            query.Select(x => x.s.Point);

            var contractTimeConfig = await _dbContext.ContractTimeConfigEntities.Where(x => x.FromDate < DateTime.Now && x.ToDate > DateTime.Now).FirstOrDefaultAsync();

            foreach (var item in query)
            {
                if(item.a.RoomId.HasValue)
                {
                    continue;
                }
                item.a.AdminConfirmStatus = confirmStatus;
                if (contractTimeConfig != null && confirmStatus == DataConfigConstant.contractConfirmStatusApprove)
                {
                    item.a.ToDate = contractTimeConfig.ToDate;
                }
                if (confirmStatus == DataConfigConstant.contractConfirmStatusReject)
                {
                    item.a.ToDate = null;
                }
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> AdminConfirmContract(int contractId, int confirmStatus)
        {
            var contract = await _dbContext.ContractEntities.FindAsync(contractId);
            var contractTimeConfig = await _dbContext.ContractTimeConfigEntities.Where(x => x.FromDate < DateTime.Now && x.ToDate > DateTime.Now).FirstOrDefaultAsync();
            if(contractTimeConfig != null && confirmStatus == DataConfigConstant.contractConfirmStatusApprove)
            {
                contract.ToDate = contractTimeConfig.ToDate;
            }
            if(confirmStatus == DataConfigConstant.contractConfirmStatusReject)
            {
                contract.ToDate = null;
            }
            if(contract != null)
            {
                contract.AdminConfirmStatus = confirmStatus;
                return await _dbContext.SaveChangesAsync();
            }
            return 0;
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
                DesiredPrice = request.DesiredPrice,
                StudentId = request.StudentId,
                ServiceId = request.ServiceId,
                AdminConfirmStatus = request.AdminConfirmStatus.HasValue ? request.AdminConfirmStatus.Value : DataConfigConstant.contractConfirmStatusPending,
                StudentConfirmStatus = request.StudentConfirmStatus.HasValue ? request.StudentConfirmStatus : DataConfigConstant.contractConfirmStatusPending,
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
                    DesiredPrice = x.DesiredPrice,
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
                        join r in _dbContext.RoomEntities on a.RoomId equals r.Id into ra
                        from r in  ra.DefaultIfEmpty()
                        join e in _dbContext.AreaEntities on r.AreaId equals e.Id into re
                        from e in re.DefaultIfEmpty()
                        select new { a, s, r, e};

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
                    DesiredPrice = x.a.DesiredPrice,
                    StudentId = x.a.StudentId,
                    StudentName = x.s.Name,
                    StudentCode = x.s.StudentCode,
                    StudentPhone = x.s.Phone,
                    Gender = x.s.Gender,
                    Adress = x.s.Adress,
                    AdminConfirmStatus = x.a.AdminConfirmStatus,
                    StudentConfirmStatus = x.a.StudentConfirmStatus,
                    RoomId = x.a.RoomId,
                    RoomName = x.r != null ? x.r.Name : null,
                    AreaName = x.e != null ? x.e.Name : null,
                    Point = x.s.Point,
                    AcademicYear = x.s.AcademicYear
                    
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
        public async Task<PageResult<ContractPendingDto>> GetListAdminConfirmContractPending(PageRequestBase request)
        {
            var query = from a in _dbContext.ContractEntities
                        where a.AdminConfirmStatus == DataConfigConstant.contractConfirmStatusApprove
                        join s in _dbContext.StudentEntities on a.StudentId equals s.Id
                        join r in _dbContext.RoomEntities on a.RoomId equals r.Id into ra
                        from r in ra.DefaultIfEmpty()
                        join e in _dbContext.AreaEntities on r.AreaId equals e.Id into re
                        from e in re.DefaultIfEmpty()
                        select new { a, s, r, e };

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
                    DesiredPrice = x.a.DesiredPrice,
                    StudentId = x.a.StudentId,
                    StudentName = x.s.Name,
                    StudentCode = x.s.StudentCode,
                    StudentPhone = x.s.Phone,
                    Gender = x.s.Gender,
                    Adress = x.s.Adress,
                    AdminConfirmStatus = x.a.AdminConfirmStatus,
                    StudentConfirmStatus = x.a.StudentConfirmStatus,
                    RoomId = x.a.RoomId,
                    RoomName = x.r != null ? x.r.Name : null,
                    AreaName = x.e != null ? x.e.Name : null,
                    Point = x.s.Point,
                    AcademicYear = x.s.AcademicYear

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

        public async Task<int> ScheduleRoom(int contractId)
        {
            var contract = await _dbContext.ContractEntities.FindAsync(contractId);
            if(contract == null)
            {
                return 0;
            }
            var student = await _dbContext.StudentEntities.FindAsync(contract.StudentId);

            var listRoom = await _dbContext.RoomEntities
                  .Where(x => x.AvaiableSlot >0)
                  .OrderByDescending(x => x.FilledSlot).ToListAsync();

            //truong hop khong con phong nao trong
            if (listRoom.Count == 0)
            {
                return 0;
            }

            if(listRoom.Where(x => x.RoomGender.HasValue && x.RoomAcedemic.HasValue).ToList().Count() > 0)
            {
                listRoom = listRoom.Where(x => x.RoomGender.HasValue && x.RoomAcedemic.HasValue).ToList();
                //lay phong theo 3 dkien: gioi tinh, khoa, gia phong mong muon
                if(contract.DesiredPrice.HasValue)
                {
                    listRoom = listRoom.Where(x => x.RoomGender.Value == student.Gender && x.RoomAcedemic.Value == student.AcademicYear && x.Price == contract.DesiredPrice).ToList();
                    //neu khong co phong thoa man tien phong mong muon
                    if (listRoom.Count == 0)
                    {
                        //neu khong co phong thoa man sinh vien khoa
                        listRoom = listRoom.Where(x => x.RoomGender.Value == student.Gender && x.RoomAcedemic.Value == student.AcademicYear).ToList();
                        if(listRoom.Count == 0)
                        {
                            listRoom = listRoom.Where(x => x.RoomGender.Value == student.Gender).ToList();
                            if(listRoom.Count == 0)
                            {
                                listRoom = new List<RoomEntity>();
                            }
                        }
                    }
                }
                else
                {
                    listRoom = listRoom.Where(x => x.RoomGender.Value == student.Gender && x.RoomAcedemic.Value == student.AcademicYear).ToList();
                    if(listRoom.Count == 0)
                    {
                        listRoom = listRoom.Where(x => x.RoomGender.Value == student.Gender).ToList();
                        if(listRoom.Count == 0)
                        {
                            listRoom = new List<RoomEntity>();
                        }
                    }
                }
            }
            if(listRoom.Count == 0)
            {
                listRoom = await _dbContext.RoomEntities
                  .Where(x => x.AvaiableSlot > 0 && !x.RoomGender.HasValue && !x.RoomAcedemic.HasValue)
                  .OrderByDescending(x => x.FilledSlot).ToListAsync();
            }

            var emptyRoom = listRoom.FirstOrDefault();
            if(!contract.RoomId.HasValue)
            {
                emptyRoom.AvaiableSlot -= 1;
            }
            contract.RoomId = emptyRoom.Id;

            emptyRoom.RoomGender = student.Gender;
            emptyRoom.RoomAcedemic = student.AcademicYear;
            
            return await _dbContext.SaveChangesAsync();
        }
    }
}
