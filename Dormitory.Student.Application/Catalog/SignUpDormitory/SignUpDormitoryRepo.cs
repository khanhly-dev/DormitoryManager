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
                }
            }
            return await _dbContext.SaveChangesAsync();
        }
    }
}
