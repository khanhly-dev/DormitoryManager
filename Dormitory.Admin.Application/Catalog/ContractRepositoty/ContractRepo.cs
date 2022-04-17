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

        public async Task<int> AdminConfirmAllContract()
        {
            var confirmStatus = DataConfigConstant.contractConfirmStatusApprove;
            var maleConfirmCount = 0;
            var femaleConfirmCount = 0;
            var confirmCount = 0;

            var listContract = await (from a in _dbContext.ContractEntities
                               //where a.AdminConfirmStatus == DataConfigConstant.contractConfirmStatusPending
                               join s in _dbContext.StudentEntities on a.StudentId equals s.Id
                               where a.IsDeleted == false
                               orderby s.Point descending
                               select new { a, s }).ToListAsync();

            var contractTimeConfig = await _dbContext.ContractTimeConfigEntities.Where(x => x.FromDate < DateTime.Now && x.ToDate > DateTime.Now).FirstOrDefaultAsync();
            var listEmptyRoom = await _dbContext.RoomEntities.Where(x => x.AvaiableSlot > 0).ToListAsync();

            var maleEmpltyRoom = listEmptyRoom.Where(x => x.RoomGender == DataConfigConstant.male).ToList();
            var feMaleEmpltyRoom = listEmptyRoom.Where(x => x.RoomGender == DataConfigConstant.female).ToList();
            var empltyRoom = listEmptyRoom.Where(x => x.RoomGender == null).ToList();

            foreach (var item in listContract)
            {
                if(item.a.RoomId.HasValue)
                {
                    continue;
                }
                if(item.s.Gender == DataConfigConstant.female && maleConfirmCount < maleEmpltyRoom.Count)
                {
                    maleConfirmCount++;
                    item.a.AdminConfirmStatus = confirmStatus;
                    if (contractTimeConfig != null && confirmStatus == DataConfigConstant.contractConfirmStatusApprove)
                    {
                        item.a.ToDate = contractTimeConfig.ToDate;
                    }
                    if (confirmStatus == DataConfigConstant.contractConfirmStatusReject)
                    {
                        item.a.ToDate = null;
                    }
                    continue;
                }
                if (item.s.Gender == DataConfigConstant.male && femaleConfirmCount < feMaleEmpltyRoom.Count)
                {
                    femaleConfirmCount++;
                    item.a.AdminConfirmStatus = confirmStatus;
                    if (contractTimeConfig != null && confirmStatus == DataConfigConstant.contractConfirmStatusApprove)
                    {
                        item.a.ToDate = contractTimeConfig.ToDate;
                    }
                    if (confirmStatus == DataConfigConstant.contractConfirmStatusReject)
                    {
                        item.a.ToDate = null;
                    }
                    continue;
                }
                if(empltyRoom.Count > 0)
                {
                    if(confirmCount < empltyRoom.Count)
                    {
                        confirmCount++;
                        item.a.AdminConfirmStatus = confirmStatus;
                        if (contractTimeConfig != null && confirmStatus == DataConfigConstant.contractConfirmStatusApprove)
                        {
                            item.a.ToDate = contractTimeConfig.ToDate;
                        }
                        if (confirmStatus == DataConfigConstant.contractConfirmStatusReject)
                        {
                            item.a.ToDate = null;
                        }
                        continue;
                    }
                }
                item.a.AdminConfirmStatus = DataConfigConstant.contractConfirmStatusReject;
            }
            await _dbContext.SaveChangesAsync();
            return maleConfirmCount + femaleConfirmCount + confirmCount;
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
                AdminConfirmStatus = request.AdminConfirmStatus.HasValue ? request.AdminConfirmStatus.Value : DataConfigConstant.contractConfirmStatusPending,
                StudentConfirmStatus = request.StudentConfirmStatus.HasValue ? request.StudentConfirmStatus : DataConfigConstant.contractConfirmStatusPending,
                IsDeleted = false
            };
            
            _dbContext.ContractEntities.Add(criterial);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var contract = await _dbContext.ContractEntities.FindAsync(id);
            if (contract != null)
            {
                contract.IsDeleted = true;
                if(contract.IsExtendContract)
                {
                    var contractService = await _dbContext.ServiceContractEntities.Where(x => x.ContractId == contract.Id).ToListAsync();
                    var contractFee = await _dbContext.ContractFeeEntities.FirstOrDefaultAsync(x => x.ContractId == contract.Id);
                    _dbContext.ContractFeeEntities.Remove(contractFee);
                    _dbContext.ServiceContractEntities.RemoveRange(contractService);                    
                    //await UpdateRoomStatus(contract.Id);
                }
                else
                {
                    var room = await _dbContext.RoomEntities.FirstOrDefaultAsync(x => x.Id == contract.RoomId);
                    room.EmptySlot += 1;
                    room.AvaiableSlot += 1;
                    room.FilledSlot -= 1;
                    if(room.FilledSlot == 0)
                    {
                        room.RoomGender = null;
                        room.RoomAcedemic = null;
                    }
                    var contractFee = await _dbContext.ContractFeeEntities.FirstOrDefaultAsync(x => x.ContractId == contract.Id);
                    _dbContext.ContractFeeEntities.Remove(contractFee);
                }    
            }
            //neu la hop dong gia han thi khi huy hop dong p xoa not phi hop dong
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<PageResult<ContractDto>> GetListCompletedContract(PageRequestBase request)
        {
            var query = from a in _dbContext.ContractEntities
                        where a.ContractCompletedStatus == DataConfigConstant.contractCompletedStatusOk
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
                .Select(x => new ContractDto()
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
                    AdminConfirmStatus = x.a.AdminConfirmStatus.Value,
                    StudentConfirmStatus = x.a.StudentConfirmStatus.Value,
                    RoomId = x.a.RoomId.Value,
                    RoomName = x.r != null ? x.r.Name : null,
                    AreaName = x.e != null ? x.e.Name : null,
                    Point = x.s.Point,
                    AcademicYear = x.s.AcademicYear,
                    ContractCompletedStatus = x.a.ContractCompletedStatus.Value,
                    FromDate = x.a.FromDate.Value,
                    ToDate = x.a.ToDate.Value,
                    RoomPrice = x.a.RoomId.HasValue ? x.r.Price : null,
                    IsExtendContract = x.a.IsExtendContract,
                    IsDelete = x.a.IsDeleted,
                    IsSummerContract = x.a.IsSummerSemesterContract
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
                        where a.IsDeleted == false
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
                    AdminConfirmStatus = x.a.AdminConfirmStatus.Value,
                    StudentConfirmStatus = x.a.StudentConfirmStatus.Value,
                    RoomId = x.a.RoomId.Value,
                    RoomName = x.r != null ? x.r.Name : null,
                    AreaName = x.e != null ? x.e.Name : null,
                    Point = x.s.Point,
                    AcademicYear = x.s.AcademicYear,
                    ContractCompletedStatus = x.a.ContractCompletedStatus.Value,
                    IsExtendContract = x.a.IsExtendContract
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
                        where a.AdminConfirmStatus == DataConfigConstant.contractConfirmStatusApprove && a.IsDeleted == false && a.ContractCompletedStatus != DataConfigConstant.contractCompletedStatusOk
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
                    AcademicYear = x.s.AcademicYear,
                    ContractCompletedStatus = x.a.ContractCompletedStatus,
                    IsExtendContract = x.a.IsExtendContract
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

        public async Task<int> ChangeRoom(int contractId, int roomId)
        {
            var contract = await _dbContext.ContractEntities.FindAsync(contractId);
            if(contract.RoomId == roomId)
            {
                return 0;
            }
            var student = await _dbContext.StudentEntities.FirstOrDefaultAsync(x => x.Id == contract.StudentId);
            var oldRoom = await _dbContext.RoomEntities.FirstOrDefaultAsync(x => x.Id == contract.RoomId);
            var newRoom = await _dbContext.RoomEntities.FirstOrDefaultAsync(x => x.Id == roomId);
            if(contract != null)
            {
                var contractFee = await _dbContext.ContractFeeEntities.FirstOrDefaultAsync(x => x.ContractId == contract.Id);
                if (contract.IsExtendContract == true && contractFee.IsPaid == false)
                {
                    //tinh tien phong
                    float roomFee = 0;
                    var totalDayContract = (contract.ToDate.Value - contract.FromDate.Value).Days;
                    double totalMonth = (int)totalDayContract / 30;
                    var dayLeft = totalDayContract % 30;
                    if (dayLeft >= 15)
                    {
                        totalMonth += 1;
                    }
                    if (dayLeft < 15)
                    {
                        totalMonth += 0.5;
                    }
                    var room = _dbContext.RoomEntities.Find(roomId);
                    if (room != null)
                    {
                        roomFee = (float)(totalMonth * room.Price);
                    }
                    contractFee.RoomPrice = roomFee;
                    contractFee.ContractPriceValue = contractFee.ServicePrice + roomFee;
                }
                oldRoom.EmptySlot += 1;
                oldRoom.FilledSlot -= 1;
                oldRoom.AvaiableSlot += 1;
                newRoom.EmptySlot -= 1;
                newRoom.FilledSlot += 1;
                newRoom.AvaiableSlot -= 1;
                if(oldRoom.FilledSlot == 0)
                {
                    oldRoom.RoomAcedemic = null;
                    oldRoom.RoomGender = null;
                }
                if(newRoom.FilledSlot == 1)
                {
                    newRoom.RoomAcedemic = student.AcademicYear;
                    newRoom.RoomGender = student.Gender;
                }
                contract.RoomId = roomId;
                return await _dbContext.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> UpdateRoomStatus(int contractId)
        {
            var contract = await _dbContext.ContractEntities.FirstOrDefaultAsync(x => x.Id == contractId);
            if(contract.RoomId.HasValue)
            {
                var room = await _dbContext.RoomEntities.FirstOrDefaultAsync(x => x.Id == contract.RoomId);
                //hop dong het han
                if((contract.IsDeleted == false && contract.ToDate < DateTime.Now) || contract.IsDeleted == true)
                {
                    if(contract.ContractCompletedStatus == DataConfigConstant.contractCompletedStatusOk)
                    {
                        room.FilledSlot -= 1;
                        room.EmptySlot += 1;
                    }
                    room.AvaiableSlot += 1;
                    if(room.FilledSlot == 0)
                    {
                        room.RoomAcedemic = null;
                        room.RoomGender = null;
                    }
                }
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ContractFeeStatusDto>> GetListContractByStudentId(int studentId)
        {
            var query = from a in _dbContext.ContractEntities
                        where a.ContractCompletedStatus == DataConfigConstant.contractCompletedStatusOk && a.StudentId == studentId
                        join s in _dbContext.StudentEntities on a.StudentId equals s.Id
                        join r in _dbContext.RoomEntities on a.RoomId equals r.Id into ra
                        from r in ra.DefaultIfEmpty()
                        join e in _dbContext.AreaEntities on r.AreaId equals e.Id into re
                        from e in re.DefaultIfEmpty()
                        join f in _dbContext.ContractFeeEntities on a.Id equals f.ContractId
                        where !(a.IsExtendContract == true && a.IsDeleted == true)
                        select new { a, s, r, e, f };

            int totalRow = await query.CountAsync();

            var data = await query
                .Select(x => new ContractFeeStatusDto()
                {
                    Id = x.a.Id,
                    ContractCode = x.a.ContractCode,
                    DateCreated = x.a.DateCreated,
                    StudentId = x.a.StudentId,
                    RoomId = x.a.RoomId.Value,
                    RoomName = x.r != null ? x.r.Name : null,
                    AreaName = x.e != null ? x.e.Name : null,
                    FromDate = x.a.FromDate.Value,
                    ToDate = x.a.ToDate.Value,
                    RoomPrice = x.a.RoomId.HasValue ? x.r.Price : null,
                    IsExtendContract = x.a.IsExtendContract,
                    ContractPriceValue = x.f.ContractPriceValue,
                    ServicePrice = x.f.ServicePrice,
                    PaidDate = x.f.PaidDate.Value,
                    MoneyPaid = x.f.MoneyPaid.Value,
                    IsPaid = x.f.IsPaid,
                    ContractPrice = x.f.RoomPrice
                }).ToListAsync();

            return data;
        }
    }
}
