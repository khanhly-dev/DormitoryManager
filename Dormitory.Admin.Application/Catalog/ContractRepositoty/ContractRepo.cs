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

        //public async Task<int> AdminConfirmAllContract()
        //{
        //    var confirmStatus = DataConfigConstant.contractConfirmStatusApprove;
        //    var maleConfirmCount = 0;
        //    var femaleConfirmCount = 0;
        //    var confirmCount = 0;

        //    var listContract = await (from a in _dbContext.ContractEntities
        //                       where a.AdminConfirmStatus == DataConfigConstant.contractCompletedStatusOk
        //                       join s in _dbContext.StudentEntities on a.StudentId equals s.Id
        //                       where a.IsDeleted == false
        //                       orderby s.Point descending
        //                       select new { a, s }).ToListAsync();

        //    var contractTimeConfig = await _dbContext.ContractTimeConfigEntities.Where(x => x.FromDate < DateTime.Now && x.ToDate > DateTime.Now).FirstOrDefaultAsync();
        //    var listEmptyRoom = await _dbContext.RoomEntities.Where(x => x.AvaiableSlot > 0).ToListAsync();

        //    var maleEmpltyRoom = listEmptyRoom.Where(x => x.RoomGender == DataConfigConstant.male).ToList();
        //    var feMaleEmpltyRoom = listEmptyRoom.Where(x => x.RoomGender == DataConfigConstant.female).ToList();
        //    var empltyRoom = listEmptyRoom.Where(x => x.RoomGender == null).ToList();

        //    foreach (var item in listContract)
        //    {
        //        if(item.a.RoomId.HasValue)
        //        {
        //            continue;
        //        }
        //        if(item.s.Gender == DataConfigConstant.female && maleConfirmCount < maleEmpltyRoom.Count)
        //        {
        //            maleConfirmCount++;
        //            item.a.AdminConfirmStatus = confirmStatus;
        //            if (contractTimeConfig != null && confirmStatus == DataConfigConstant.contractConfirmStatusApprove)
        //            {
        //                item.a.ToDate = contractTimeConfig.ToDate;
        //            }
        //            if (confirmStatus == DataConfigConstant.contractConfirmStatusReject)
        //            {
        //                item.a.ToDate = null;
        //            }
        //            continue;
        //        }
        //        if (item.s.Gender == DataConfigConstant.male && femaleConfirmCount < feMaleEmpltyRoom.Count)
        //        {
        //            femaleConfirmCount++;
        //            item.a.AdminConfirmStatus = confirmStatus;
        //            if (contractTimeConfig != null && confirmStatus == DataConfigConstant.contractConfirmStatusApprove)
        //            {
        //                item.a.ToDate = contractTimeConfig.ToDate;
        //            }
        //            if (confirmStatus == DataConfigConstant.contractConfirmStatusReject)
        //            {
        //                item.a.ToDate = null;
        //            }
        //            continue;
        //        }
        //        if(empltyRoom.Count > 0)
        //        {
        //            if(confirmCount < empltyRoom.Count)
        //            {
        //                confirmCount++;
        //                item.a.AdminConfirmStatus = confirmStatus;
        //                if (contractTimeConfig != null && confirmStatus == DataConfigConstant.contractConfirmStatusApprove)
        //                {
        //                    item.a.ToDate = contractTimeConfig.ToDate;
        //                }
        //                if (confirmStatus == DataConfigConstant.contractConfirmStatusReject)
        //                {
        //                    item.a.ToDate = null;
        //                }
        //                continue;
        //            }
        //        }
        //        item.a.AdminConfirmStatus = DataConfigConstant.contractConfirmStatusReject;
        //    }
        //    await _dbContext.SaveChangesAsync();
        //    return maleConfirmCount + femaleConfirmCount + confirmCount;
        //}
        public async Task<int> AdminConfirmAllContract()
        {
            var countRoomMale = 0;
            var countRoomFemale = 0;

            var listContract = await (from a in _dbContext.ContractEntities
                                      where a.AdminConfirmStatus == DataConfigConstant.contractConfirmStatusPending
                                      join s in _dbContext.StudentEntities on a.StudentId equals s.Id
                                      where a.IsDeleted == false
                                      orderby s.Point descending
                                      select new ContractConfirmDto
                                      {
                                          Id = a.Id,
                                          ContractCode = a.ContractCode,
                                          DateCreated = a.DateCreated,
                                          FromDate = a.FromDate,
                                          ToDate = a.ToDate,
                                          RoomId = a.RoomId,
                                          DesiredPrice = a.DesiredPrice,
                                          StudentId = a.StudentId,
                                          Gender = s.Gender,
                                         
                                          AdminConfirmStatus = a.AdminConfirmStatus,
                                          StudentConfirmStatus = a.StudentConfirmStatus,
                                          ContractCompletedStatus = a.ContractCompletedStatus,
                                          IsDeleted = a.IsDeleted,
                                          IsExtendContract = a.IsExtendContract,
                                          IsSummerSemesterContract = a.IsSummerSemesterContract,
                                          SemesterId = a.SemesterId
                                      }).ToListAsync();
            //lay cau hinh hop dong
            var contractTimeConfig = await _dbContext.ContractTimeConfigEntities.Where(x => x.FromDate < DateTime.Now && x.ToDate > DateTime.Now).FirstOrDefaultAsync();

            //danh sach hop dong nam
            var maleContract = listContract.Where(x => x.Gender == DataConfigConstant.male).ToList();
            //danh sach hop dong nu
            var femaleContract = listContract.Where(x => x.Gender == DataConfigConstant.female).ToList();
            //tong so phong trong cho nam
            var countEmptyRoomForMale = (await _dbContext.RoomEntities.Where(x => x.AvaiableSlot > 0 && x.RoomGender == DataConfigConstant.male).ToListAsync()).Sum(x => x.AvaiableSlot);
            //tong so phong trong cho nu
            var countEmptyRoomForFemale = (await _dbContext.RoomEntities.Where(x => x.AvaiableSlot > 0 && x.RoomGender == DataConfigConstant.female).ToListAsync()).Sum(x => x.AvaiableSlot);
            //so phong trong cho all
            var emptyRoomForAll = await _dbContext.RoomEntities.Where(x => x.AvaiableSlot > 0 && x.RoomGender == null).ToListAsync();

            //duyet cho nam
            foreach (var item in maleContract)
            {
                if(countRoomMale < countEmptyRoomForMale)
                {
                    item.AdminConfirmStatus = DataConfigConstant.contractConfirmStatusApprove;
                    if (contractTimeConfig != null)
                    {
                        item.ToDate = contractTimeConfig.ToDate;
                        item.SemesterId = contractTimeConfig.Id;
                    }
                    countRoomMale++;
                }
                else
                {
                    break;
                }
            }
            //duyet cho nu
            foreach (var item in femaleContract)
            {
                if (countRoomFemale < countEmptyRoomForFemale)
                {
                    item.AdminConfirmStatus = DataConfigConstant.contractConfirmStatusApprove;
                    if (contractTimeConfig != null)
                    {
                        item.ToDate = contractTimeConfig.ToDate;
                        item.SemesterId = contractTimeConfig.Id;
                    }
                    countRoomFemale++;
                }
                else
                {
                    break;
                }
            }
            //so hop dong nam con lai chua duoc duyet
            var nonConfirmMaleContract = new List<ContractConfirmDto>();
            //so hop dong nu con lai chua duoc duyet
            var nonConfirmFemaleContract = new List<ContractConfirmDto>();
            //duyet qua tung phong

            bool genderExamine = true;
            foreach (var item in emptyRoomForAll)
            {
                nonConfirmMaleContract = maleContract.Where(x => x.AdminConfirmStatus == null || x.AdminConfirmStatus == 0).ToList();
                nonConfirmFemaleContract = femaleContract.Where(x => x.AdminConfirmStatus == null || x.AdminConfirmStatus == 0).ToList();

                if(nonConfirmMaleContract.Count == 0 && nonConfirmFemaleContract.Count == 0)
                {
                    break;
                }

                //duyệt đan xen nam nữ
                genderExamine = !genderExamine;
                //nếu duyệt xong nữ rồi thì sẽ duyệt nam và ngược lại
                if(nonConfirmFemaleContract.Count == 0)
                {
                    genderExamine = true;
                }    
                if(nonConfirmMaleContract.Count == 0)
                {
                    genderExamine = false;
                }    

                if (nonConfirmMaleContract.Count > 0 && genderExamine == true)
                {
                    foreach (var childItem in nonConfirmMaleContract.Take(item.AvaiableSlot ?? 0).ToList())
                    {
                        childItem.AdminConfirmStatus = DataConfigConstant.contractConfirmStatusApprove;
                        childItem.ToDate = contractTimeConfig.ToDate;
                        childItem.SemesterId = contractTimeConfig.Id;
                    } 
                    continue;
                }   
                if(nonConfirmFemaleContract.Count > 0 && genderExamine == false)
                {
                    foreach (var childItem in nonConfirmFemaleContract.Take(item.AvaiableSlot ?? 0).ToList())
                    {
                        childItem.AdminConfirmStatus = DataConfigConstant.contractConfirmStatusApprove;
                        childItem.ToDate = contractTimeConfig.ToDate;
                        childItem.SemesterId = contractTimeConfig.Id;
                    }
                    continue;
                }
            }

            var rejectConfirm = listContract.Where(x => x.AdminConfirmStatus == null || x.AdminConfirmStatus == 0).ToList();
            foreach (var item in rejectConfirm)
            {
                item.AdminConfirmStatus = DataConfigConstant.contractConfirmStatusReject;
            }
            var listUpdate = listContract.Select(x => new ContractEntity
            {
                Id = x.Id,
                ContractCode = x.ContractCode,
                DateCreated = x.DateCreated,
                FromDate = x.FromDate,
                ToDate = x.ToDate,
                RoomId = x.RoomId,
                DesiredPrice = x.DesiredPrice,
                StudentId = x.StudentId,
                AdminConfirmStatus = x.AdminConfirmStatus,
                StudentConfirmStatus = x.StudentConfirmStatus,
                ContractCompletedStatus = x.ContractCompletedStatus,
                IsDeleted = x.IsDeleted,
                IsExtendContract = x.IsExtendContract,
                IsSummerSemesterContract = x.IsSummerSemesterContract,
                SemesterId = x.SemesterId
            }).ToList();

            _dbContext.ContractEntities.UpdateRange(listUpdate);

            await _dbContext.SaveChangesAsync();

            return listContract.Where(x => x.AdminConfirmStatus == DataConfigConstant.contractConfirmStatusApprove).ToList().Count;
        }

        public async Task<int> AdminConfirmContract(int contractId, int confirmStatus)
        {
            var contract = await _dbContext.ContractEntities.FindAsync(contractId);
            var contractTimeConfig = await _dbContext.ContractTimeConfigEntities.Where(x => x.FromDate < DateTime.Now && x.ToDate > DateTime.Now).FirstOrDefaultAsync();
            if(contractTimeConfig != null && confirmStatus == DataConfigConstant.contractConfirmStatusApprove)
            {
                contract.AdminConfirmStatus = confirmStatus;
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
                    IsSummerContract = x.a.IsSummerSemesterContract,
                    SemesterId = x.a.SemesterId,
                    AreaId = x.e != null ? x.e.Id : null,
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
                        where a.IsDeleted == false && a.ContractCompletedStatus != DataConfigConstant.contractCompletedStatusOk
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
                    RoomPrice = x.r != null ? x.r.Price : null,
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
            var contract = await _dbContext.ContractEntities.FirstOrDefaultAsync(x => x.Id == contractId);
            if(contract.RoomId.HasValue)
            {
                return 0;
            }
            if(!contract.DesiredPrice.HasValue)
            {
                contract.DesiredPrice = 0;
            }
            var student = await _dbContext.StudentEntities.FirstOrDefaultAsync(x => x.Id == contract.StudentId);

            #region luồng xếp vào phòng đã có người ở
            //tìm phòng theo tiêu chí: giới tính, khoá học, giá mong muốn
            var emptyRoom = await _dbContext.RoomEntities
                    .Where(x => x.RoomGender == student.Gender && x.RoomAcedemic == student.AcademicYear && x.Price == contract.DesiredPrice && x.AvaiableSlot > 0)
                    .OrderBy(x => x.AvaiableSlot)
                    .FirstOrDefaultAsync();

            // nếu không có phòng trống thì bỏ điều kiện cùng khóa
            if(emptyRoom == null)
            {
                emptyRoom = await _dbContext.RoomEntities
                    .Where(x => x.RoomGender == student.Gender && x.Price == contract.DesiredPrice && x.AvaiableSlot > 0)
                    .OrderBy(x => x.AvaiableSlot)
                    .FirstOrDefaultAsync();

                if(emptyRoom == null)
				{
                    emptyRoom = await _dbContext.RoomEntities
                        .Where(x => x.RoomGender == null && x.Price == contract.DesiredPrice && x.AvaiableSlot > 0)
                        .OrderBy(x => x.AvaiableSlot)
                        .FirstOrDefaultAsync();

                    if (emptyRoom == null)
                    {
                        //nếu không còn phòng trống thì bỏ điều kiện giá mong muốn
                        if (emptyRoom == null)
                        {
                            emptyRoom = await _dbContext.RoomEntities
                               .Where(x => x.RoomGender == student.Gender && x.AvaiableSlot > 0)
                               .OrderBy(x => x.AvaiableSlot)
                               .FirstOrDefaultAsync();
                        }
                    }
                }
            }   

            if(emptyRoom != null)
            {
                contract.RoomId = emptyRoom.Id;
                if (contract.DesiredPrice == 0)
                {
                    contract.DesiredPrice = null;
                }
                emptyRoom.AvaiableSlot -= 1;
                emptyRoom.RoomGender = student.Gender;
                emptyRoom.RoomAcedemic = student.AcademicYear;
                return await _dbContext.SaveChangesAsync();
            }
            #endregion

            #region luồng xếp vào phòng mới chưa có người ở
            //tìm phòng theo tiêu chí giá mong muốn
            emptyRoom = await _dbContext.RoomEntities
                    .Where(x => x.RoomGender == null && x.Price == contract.DesiredPrice && x.AvaiableSlot > 0)
                    .OrderBy(x => x.AvaiableSlot)
                    .FirstOrDefaultAsync();

            // nếu không có phòng trống thì bỏ điều kiện giá mong muốn
            if(emptyRoom == null)
            {
                emptyRoom = await _dbContext.RoomEntities
                    .Where(x => x.RoomGender == null && x.AvaiableSlot > 0)
                    .OrderBy(x => x.AvaiableSlot)
                    .FirstOrDefaultAsync();
            }    

            if(emptyRoom != null)
            {
                contract.RoomId = emptyRoom.Id;
                if(contract.DesiredPrice == 0)
                {
                    contract.DesiredPrice = null;
                }
                emptyRoom.AvaiableSlot -= 1;
                emptyRoom.RoomGender = student.Gender;
                emptyRoom.RoomAcedemic = student.AcademicYear;

                return await _dbContext.SaveChangesAsync();
            }
            #endregion

            return 0;
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
