using Dormitory.Admin.Application.Catalog.StudentRepository.Dtos;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntites;
using Dormitory.Domain.AppEntities;
using Dormitory.Domain.Shared.Constant;
using Dormitory.EntityFrameworkCore.AdminEntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.StudentRepository
{
    public class StudentRepo : IStudentRepo
    {
        private readonly AdminSolutionDbContext _dbContext;
        public StudentRepo(AdminSolutionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddDiscipline(DisciplineEntity request)
        {
            if(request.Id == 0)
            {
                _dbContext.DisciplineEntities.Add(request);
            }
            else
            {
                _dbContext.DisciplineEntities.Update(request);
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> CreateOrUpdate(StudentEntity request)
        {
            var student = new StudentEntity()
            {
                Id = request.Id,
                Name = request.Name,
                DOB = request.DOB,
                BaseAdress = request.BaseAdress,
                Adress = request.Adress,
                Class = request.Class,
                Phone = request.Phone,
                Email = request.Email,
                Major = request.Major,
                Gender = request.Gender,
                AcademicYear = request.AcademicYear,
                Ethnic = request.Ethnic,
                RelativeName = request.RelativeName,
                RelativePhone = request.RelativePhone,
                Religion = request.Religion,
                StudentCode = request.StudentCode,
                Point = request.Point.HasValue ? request.Point.Value : 0,
                IsStudying = request.IsStudying,
            };
            if (student.Id == 0)
            {
                _dbContext.StudentEntities.Add(student);
                return await _dbContext.SaveChangesAsync();
            }
            else if (student.Id > 0)
            {
                _dbContext.StudentEntities.Update(student);
                return await _dbContext.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> Delete(int id)
        {
            var student = await _dbContext.StudentEntities.FindAsync(id);
            if (student != null)
            {
                _dbContext.StudentEntities.Remove(student);
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteDiscipline(int disciplineId)
        {
            var discipline = await _dbContext.DisciplineEntities.FindAsync(disciplineId);
            if(discipline != null)
            {
                _dbContext.DisciplineEntities.Remove(discipline);
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<PageResult<StudentDto>> GetAll(PageRequestBase request)
        {
            var query = from s in _dbContext.StudentEntities
                        select s;

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Name.Contains(request.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new StudentDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Phone = x.Phone,
                    Email = x.Email,
                    Major = x.Major,
                    Ethnic = x.Ethnic,
                    AcademicYear = x.AcademicYear,
                    Adress = x.Adress,
                    BaseAdress = x.BaseAdress,
                    DOB = x.DOB,
                    RelativeName = x.RelativeName,
                    RelativePhone = x.RelativePhone,
                    Religion = x.Religion,
                    Class = x.Class,
                    Gender = x.Gender,
                    StudentCode = x.StudentCode,
                    Point = x.Point,
                    PaymentStatus = true
                }).ToListAsync();

            var pageResult = new PageResult<StudentDto>()
            {
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                TotalRecords = totalRow,
                Items = data
            };
            return pageResult;
        }

        public async Task<PageResult<StudentDto>> GetList(PageRequestBase request)
        {
            var query = from s in _dbContext.StudentEntities
                        select s ;

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Name.Contains(request.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new StudentDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Phone = x.Phone,
                    Email = x.Email,
                    Major = x.Major,
                    Ethnic = x.Ethnic,
                    AcademicYear = x.AcademicYear,
                    Adress = x.Adress,
                    BaseAdress = x.BaseAdress,
                    DOB = x.DOB,
                    RelativeName = x.RelativeName,
                    RelativePhone = x.RelativePhone,
                    Religion = x.Religion,
                    Class = x.Class,
                    Gender = x.Gender,
                    StudentCode = x.StudentCode,
                    Point = x.Point,
                    PaymentStatus = true
                }).ToListAsync();

            var temp = new List<StudentDto>();
            foreach (var item in data)
            {
                var listContract = await _dbContext.ContractEntities.Where(x => x.StudentId == item.Id).ToListAsync();
                var listFee = await _dbContext.ContractFeeEntities.Where(t => listContract.Select(x => x.Id).Contains(t.ContractId)).ToListAsync();

                item.Dept = listFee.Sum(x => x.ContractPriceValue) - listFee.Sum(x => x.MoneyPaid.Value);

                if(listFee.Where(x => x.IsPaid == false).ToList().Count > 0)
                {
                    item.PaymentStatus = false;
                }
                else
                {
                    item.PaymentStatus = true;
                }
                if (listContract.Where(x => x.ContractCompletedStatus == DataConfigConstant.contractCompletedStatusOk).ToList().Count == 0)
                {
                    temp.Add(item);
                }
            }
            foreach (var item in temp)
            {
                data.Remove(item);
            }

            var pageResult = new PageResult<StudentDto>()
            {
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                TotalRecords = totalRow,
                Items = data
            };
            return pageResult;
        }

        public async Task<PageResult<DisciplineDto>> GetListDiscipline(PageRequestBase request)
        {
            var query = from s in _dbContext.StudentEntities
                        join d in _dbContext.DisciplineEntities on s.Id equals d.StudentId
                        select new { d, s };

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.s.Name.Contains(request.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new DisciplineDto()
                {
                    StudentId = x.d.StudentId,
                    Name = x.s.Name,
                    Phone = x.s.Phone,
                    Major = x.s.Major,
                    DOB = x.s.DOB,
                    Class = x.s.Class,
                    Gender = x.s.Gender,
                    StudentCode = x.s.StudentCode,
                    Id =x.d.Id,
                    Description = x.d.Description,
                    Punish = x.d.Punish,
                }).ToListAsync();

            foreach (var item in data)
            {
                var contract = await _dbContext.ContractEntities.FirstOrDefaultAsync(x => x.ContractCompletedStatus == DataConfigConstant.contractCompletedStatusOk && x.IsExtendContract == false);
                if (contract != null)
                {
                    var room = await _dbContext.RoomEntities.FirstOrDefaultAsync(x => x.Id == contract.RoomId);
                    var area = await _dbContext.AreaEntities.FirstOrDefaultAsync(x => x.Id == room.AreaId);
                    item.RoomName = room.Name;
                    item.AreaName = area.Name;
                }
            }

            var pageResult = new PageResult<DisciplineDto>()
            {
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                TotalRecords = totalRow,
                Items = data
            };
            return pageResult;
        }

        public async Task<List<ComboSelectDto>> GetListStudentSelect()
        {
            var listStudent = await _dbContext.StudentEntities.Select(x => new ComboSelectDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            return listStudent;
        }

        public async Task<int> UpdateContractFee(int contractId, DateTime datePaid, float moneyPaid)
        {
            var contractFee = await _dbContext.ContractFeeEntities.FirstOrDefaultAsync(x => x.ContractId == contractId);
            if (contractFee == null)
            {
                return 0;
            }
            contractFee.PaidDate = datePaid;
            contractFee.MoneyPaid = moneyPaid;
            if(moneyPaid >= contractFee.ContractPriceValue)
            {
                contractFee.IsPaid = true;
            }
            else
            {
                contractFee.IsPaid = false;
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateDiscipline(DisciplineEntity request)
        {
            _dbContext.DisciplineEntities.Update(request);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
