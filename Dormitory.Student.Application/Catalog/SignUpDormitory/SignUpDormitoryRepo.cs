using Dormitory.Domain.AppEntites;
using Dormitory.Domain.AppEntities;
using Dormitory.Domain.Shared.Constant;
using Dormitory.EntityFrameworkCore.AdminEntityFrameworkCore;
using Dormitory.Student.Application.Catalog.SignUpDormitory.Dtos;
using Dormitory.Student.Application.Catalog.SignUpDormitory.Requests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Student.Application.Catalog.SignUpDormitory
{
    public class SignUpDormitoryRepo : ISignUpDormitoryRepo
    {
        private readonly AdminSolutionDbContext _dbContext;
        public SignUpDormitoryRepo(AdminSolutionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SignUp(SignUpRequest request)
        {
            var listCode = await _dbContext.ContractEntities.Select(x => x.ContractCode).ToListAsync();
            var random = new Random();
            var contractCode = "HD" + random.Next(100000,999999).ToString();
            while(listCode.Contains(contractCode))
            {
                contractCode = "HD" + random.Next(100000, 999999).ToString();
            }

            var criterial = new ContractEntity()
            {
                Id = 0,
                ContractCode = contractCode,
                DateCreated = DateTime.Now,
                DesiredPrice = request.DesiredPrice,
                StudentId = request.StudentId,
                AdminConfirmStatus = DataConfigConstant.contractConfirmStatusPending,
                StudentConfirmStatus = DataConfigConstant.contractConfirmStatusPending,
                IsDeleted = false,
                IsSummerSemesterContract = false,
            };

            _dbContext.ContractEntities.Add(criterial);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> SetStudentPoint(SetStudentPointRepuest request)
        {
            if(request.ListCriteriaId == null || request.ListCriteriaId.Count == 0)
            {
                return 0;
            }
            var studentPoint = 0;
            //xoa tat ca ban ghi cham diem cu
            var listStudentCriteria =  _dbContext.StudentCriteriaEntities.AsNoTracking();
            _dbContext.StudentCriteriaEntities.RemoveRange(listStudentCriteria);
            await _dbContext.SaveChangesAsync();

            foreach (var item in request.ListCriteriaId)
            {
                //luu thong tin uu tien cua sinh vien vao db
                var studentCriteria = new StudentCriteriaEntity{
                    Id = 0,
                    StudentId = request.StudentId,
                    CritariaId = item
                };
                _dbContext.StudentCriteriaEntities.Add(studentCriteria);
                //tinh diem cho sinh vien
                var criteriaRecord = await _dbContext.CriteriaConfigEntities.FindAsync(item);
                if(criteriaRecord != null)
                {
                    studentPoint += criteriaRecord.Point;
                }
            }
            var student = await _dbContext.StudentEntities.FindAsync(request.StudentId);
            if(student != null)
            {
                student.Point = studentPoint;
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<CriteriaDto>> GetListCriteria()
        {
            var query = from c in _dbContext.CriteriaConfigEntities
                        select c;

            var data = await query.Select(x => new CriteriaDto
            {
                Label = x.Name,
                Value = x.Id
            }).ToListAsync();

            return data;
        }

        public async Task<int> Delete(int id)
        {
            var contract = await _dbContext.ContractEntities.FindAsync(id);
            if (contract != null)
            {
                contract.IsDeleted = false;
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> StudentConfirmContract(int contractId, int confirmStatus)
        {
            var contract = await _dbContext.ContractEntities.FindAsync(contractId);
            var student = await _dbContext.StudentEntities.FindAsync(contract.StudentId);
            if(contract != null)
            {
                contract.StudentConfirmStatus = confirmStatus;
                //neu sv tu choi thi coi nhu huy hop dong
                if(confirmStatus == DataConfigConstant.contractConfirmStatusReject)
                {
                    contract.ContractCompletedStatus = DataConfigConstant.contractCompletedStatusCancel;
                    var room = await _dbContext.RoomEntities.FindAsync(contract.RoomId);
                    if (room != null)
                    {
                        room.AvaiableSlot += 1;
                        if(room.FilledSlot == 0)
                        {
                            room.RoomGender = null;
                            room.RoomAcedemic = null;
                        }
                    }
                }
                //neu sv dong y thi cap nhat trang thai phong + cap nhat ngay gia han hop dong + cap nhat trang thai hop dong
                if (confirmStatus == DataConfigConstant.contractConfirmStatusApprove)
                {
                    contract.ContractCompletedStatus = DataConfigConstant.contractCompletedStatusOk;
                    contract.FromDate = DateTime.Now; 
                    var room = await _dbContext.RoomEntities.FindAsync(contract.RoomId);
                    if(room != null)
                    {
                        room.EmptySlot -= 1;
                        room.FilledSlot += 1;
                        if(room.FilledSlot == 1)
                        {
                            room.RoomGender = student.Gender;
                            room.RoomAcedemic = student.AcademicYear;
                        }
                    }
                    await AddContractFee(contractId);
                }
            }
            return await _dbContext.SaveChangesAsync();
        }

        private async Task<int> AddContractFee(int contractId)
        {
            var contract = await _dbContext.ContractEntities.FindAsync(contractId);
            if(contract == null)
            {
                return 0;
            }
            //add service into service contract
            var listService = _dbContext.ServiceEntities.Where(x => x.ServiceType == DataConfigConstant.ContractService);
            foreach (var item in listService)
            {
                var serviceContract = new ServiceContractEntity
                {
                    ContractId = contractId,
                    ServiceId = item.Id
                };
                _dbContext.ServiceContractEntities.Add(serviceContract);
            }
            await _dbContext.SaveChangesAsync();

            float roomFee = 0;
            float serviceContractFee = 0;
            //tinh tien phong
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
            var room = _dbContext.RoomEntities.Find(contract.RoomId);
            if(room != null)
            {
                roomFee = (float)(totalMonth * room.Price);
            }
            //tinh tien dich vu
            var listServiceContract = await _dbContext.ServiceContractEntities.Where(x => x.ContractId == contractId).ToListAsync();
            foreach (var item in listServiceContract)
            {
                var service = await _dbContext.ServiceEntities.FirstOrDefaultAsync(x => x.Id == item.ServiceId);
                serviceContractFee += (float)(service.Price * totalMonth);
            }
            //add vao bang contractFee
            var contractFee = new ContractFeeEntity
            {
                ContractId = contractId,
                RoomPrice = roomFee,
                ServicePrice = serviceContractFee,
                ContractPriceValue = roomFee + serviceContractFee,
                MoneyPaid = 0,
                IsPaid = false,
            };
            _dbContext.ContractFeeEntities.Add(contractFee);
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> CreateExtendContract(int studentId)
        {
            var contract = await _dbContext.ContractEntities.Where(x => x.StudentId == studentId && x.ToDate > DateTime.Now && x.IsDeleted == false).FirstOrDefaultAsync();
            //neu hop dong het han hoac ko co thi p dki moi
            if(contract == null)
            {
                return 0;
            }

            //gen contract code
            var listCode = await _dbContext.ContractEntities.Select(x => x.ContractCode).ToListAsync();
            var random = new Random();
            var contractCode = "HD" + random.Next(100000, 999999).ToString();
            while (listCode.Contains(contractCode))
            {
                contractCode = "HD" + random.Next(100000, 999999).ToString();
            }
            //tinh ngay ket thuc hop dong
            var contractTimeConfigToDate = await _dbContext.ContractTimeConfigEntities.Where(x => x.FromDate >= contract.ToDate && x.IsSummerSemester == false).OrderBy(x => x.ToDate).FirstOrDefaultAsync();
            var extendContract = new ContractEntity
            {
                ContractCode = contractCode,
                DateCreated = DateTime.Now,
                FromDate = contractTimeConfigToDate.FromDate,
                ToDate = contractTimeConfigToDate.ToDate,
                RoomId = contract.RoomId,
                DesiredPrice = contract.DesiredPrice,
                StudentId = contract.StudentId,
                AdminConfirmStatus = contract.AdminConfirmStatus,
                StudentConfirmStatus = contract.StudentConfirmStatus,
                ContractCompletedStatus = DataConfigConstant.contractCompletedStatusOk,
                IsDeleted = contract.IsDeleted,
                IsExtendContract = DataConfigConstant.extendContract,
                IsSummerSemesterContract = false,
                SemesterId = contractTimeConfigToDate.Id
            };
          

            _dbContext.ContractEntities.Add(extendContract);
            await _dbContext.SaveChangesAsync();

            //add dich vu va them phi hop dong
            await AddContractFee(extendContract.Id);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<ExtendContractTime> GetExtendContractTime(int studentId)
        {
            var contract = await _dbContext.ContractEntities.Where(x => x.StudentId == studentId && x.ToDate > DateTime.Now && x.IsDeleted == false).FirstOrDefaultAsync();
            //neu hop dong het han hoac ko co thi p dki moi
            if (contract == null)
            {
                return null;
            }

            var contractTimeConfigToDate = await _dbContext.ContractTimeConfigEntities.Where(x => x.FromDate >= contract.ToDate && x.IsSummerSemester == false).OrderBy(x => x.ToDate).FirstOrDefaultAsync();
            var time = new ExtendContractTime
            {
                FromDate = contractTimeConfigToDate.FromDate,
                ToDate = contractTimeConfigToDate.ToDate,
            };
            return time;
        }

        public async Task<int> CreateSummerSemesterContract(int studentId)
        {
            var contract = await _dbContext.ContractEntities.Where(x => x.StudentId == studentId && x.ToDate > DateTime.Now && x.IsDeleted == false).FirstOrDefaultAsync();
            //neu hop dong het han hoac ko co thi p dki moi
            if (contract == null)
            {
                return 0;
            }
            //gen contract code
            var listCode = await _dbContext.ContractEntities.Select(x => x.ContractCode).ToListAsync();
            var random = new Random();
            var contractCode = "HD" + random.Next(100000, 999999).ToString();
            while (listCode.Contains(contractCode))
            {
                contractCode = "HD" + random.Next(100000, 999999).ToString();
            }
            //tinh ngay ket thuc hop dong
            var contractTimeConfigToDate = await _dbContext.ContractTimeConfigEntities.Where(x => x.FromDate >= contract.ToDate && x.IsSummerSemester == true).OrderBy(x => x.ToDate).FirstOrDefaultAsync();
         
            var extendContract = new ContractEntity
            {
                ContractCode = contractCode,
                DateCreated = DateTime.Now,
                FromDate = contractTimeConfigToDate.FromDate,
                ToDate = contractTimeConfigToDate.ToDate,
                RoomId = contract.RoomId,
                DesiredPrice = contract.DesiredPrice,
                StudentId = contract.StudentId,
                AdminConfirmStatus = contract.AdminConfirmStatus,
                StudentConfirmStatus = contract.StudentConfirmStatus,
                ContractCompletedStatus = contract.ContractCompletedStatus,
                IsDeleted = contract.IsDeleted,
                IsExtendContract = false,
                IsSummerSemesterContract = true,
                SemesterId = contractTimeConfigToDate.Id
            };

            _dbContext.ContractEntities.Add(extendContract);
            await _dbContext.SaveChangesAsync();

            //add dich vu va them phi hop dong
            await AddContractFee(extendContract.Id);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<ExtendContractTime> GetSummerContractTime(int studentId)
        {
            var contract = await _dbContext.ContractEntities.Where(x => x.StudentId == studentId && x.ToDate > DateTime.Now && x.IsDeleted == false).FirstOrDefaultAsync();
            //neu hop dong het han hoac ko co thi p dki moi
            if (contract == null)
            {
                return null;
            }

            var contractTimeConfigToDate = await _dbContext.ContractTimeConfigEntities.Where(x => x.FromDate >= contract.ToDate && x.IsSummerSemester == true).OrderBy(x => x.ToDate).FirstOrDefaultAsync();
            var time = new ExtendContractTime
            {
                FromDate = contractTimeConfigToDate.FromDate,
                ToDate = contractTimeConfigToDate.ToDate,
            };
            return time;
        }
    }
}
