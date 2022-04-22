using Dormitory.Domain.AppEntities;
using Dormitory.Domain.Shared.Constant;
using Dormitory.EntityFrameworkCore.AdminEntityFrameworkCore;
using Dormitory.Student.Application.Catalog.StudentInfoRepository.Dtos;
using Dormitory.Student.Application.CommonDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Student.Application.Catalog.StudentInfoRepository
{
    public class StudentInfoRepo : IStudentInfoRepo
    {
        private readonly AdminSolutionDbContext _adminSolutionDbContext;
        public StudentInfoRepo(AdminSolutionDbContext adminSolutionDbContext)
        {
            _adminSolutionDbContext = adminSolutionDbContext;
        }

        public async Task<bool> CheckCanCreateExtendContract(int studentId)
        {
            var student = await _adminSolutionDbContext.StudentEntities.FirstOrDefaultAsync(x => x.Id == studentId);
            if (student != null)
            {
                if (student.IsStudying == false)
                {
                    return false;
                }
            }
            var contract = await _adminSolutionDbContext.ContractEntities.Where(x => x.StudentId == studentId && x.IsDeleted == false && x.IsExtendContract == true).AsNoTracking().ToListAsync();
            if (contract.Count >= 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> CheckCanCreateSummerContract(int studentId)
        {
            var student = await _adminSolutionDbContext.StudentEntities.FirstOrDefaultAsync(x => x.Id == studentId);
            if (student != null)
            {
                if (student.IsStudying == false)
                {
                    return false;
                }
            }
            var contract = await _adminSolutionDbContext.ContractEntities.Where(x => x.StudentId == studentId && x.IsDeleted == false && x.IsSummerSemesterContract == true).AsNoTracking().ToListAsync();
            if (contract.Count >= 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> CheckCanSignUp(int studentId)
        {
            var student = await _adminSolutionDbContext.StudentEntities.FirstOrDefaultAsync(x => x.Id == studentId);
            if(student!= null)
            {
                if(student.IsStudying == false)
                {
                    return false;
                }
            }
            var contract = await _adminSolutionDbContext.ContractEntities.Where(x => x.StudentId == studentId).AsNoTracking().ToListAsync();
            if (contract == null || contract.Count == 0)
            {
                return true;
            }
            else
            {
                var check = contract.Select(x => x.ToDate > DateTime.Now).ToList().Count;
                if(check > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public async Task<int> CreateStudent(StudentEntity student)
        {
            _adminSolutionDbContext.StudentEntities.Add(student);
            return await _adminSolutionDbContext.SaveChangesAsync();
        }

        public async Task<PageResult<ContractPendingDto>> GetListStudentConfirmContractPending(GetListContractByStudentIdRepuest request)
        {
            var query = from a in _adminSolutionDbContext.ContractEntities
                        where a.StudentId == request.StudentId 
                        join s in _adminSolutionDbContext.StudentEntities on a.StudentId equals s.Id
                        join r in _adminSolutionDbContext.RoomEntities on a.RoomId equals r.Id into ra
                        from r in ra.DefaultIfEmpty()
                        join e in _adminSolutionDbContext.AreaEntities on r.AreaId equals e.Id into re
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
                    AdminConfirmStatus = x.a.AdminConfirmStatus.Value,
                    StudentConfirmStatus = x.a.StudentConfirmStatus.Value,
                    RoomId = x.a.RoomId.Value,
                    RoomName = x.r != null ? x.r.Name : null,
                    AreaName = x.e != null ? x.e.Name : null,
                    Point = x.s.Point.Value,
                    AcademicYear = x.s.AcademicYear,
                    ToDate = x.a.ToDate.Value,
                    FromDate = x.a.FromDate.Value,
                    ContractCompletedStatus = x.a.ContractCompletedStatus.Value,
                    IsExtendContract = x.a.IsExtendContract,
                    RoomPrice = x.a.RoomId.HasValue ? x.r.Price : null,
                    IsDelete = x.a.IsDeleted,
                    IsSummerContract = x.a.IsSummerSemesterContract
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

        public async Task<List<float>> GetRecomendRoomPrice()
        {
            var data = await (from r in _adminSolutionDbContext.RoomEntities
                              select r.Price).ToListAsync();
            var result = data.GroupBy(x => x)
                             .Select(grp => grp.First())
                             .ToList();
            return result;
        }

        public async Task<StudentInfoDto> GetStudentByUserId(int userId)
        {
            var query = from u in _adminSolutionDbContext.UserAccountEntities
                        where u.Id == userId
                        join s in _adminSolutionDbContext.StudentEntities on u.UserInfoId equals s.Id
                        select new { u, s };

            var data = await query.Select(x => new StudentInfoDto
            {
                UserId = x.u.Id,
                UserName = x.u.UserName,
                Id = x.s.Id,
                Name = x.s.Name,
                DOB = x.s.DOB,
                BaseAdress = x.s.BaseAdress,
                Adress = x.s.Adress,
                Class = x.s.Class,
                Phone = x.s.Phone,
                Email = x.s.Email,
                StudentCode = x.s.StudentCode,
                Major = x.s.Major,
                Gender = x.s.Gender,
                AcademicYear = x.s.AcademicYear,
                RelativeName = x.s.RelativeName,
                RelativePhone = x.s.RelativePhone,
                Religion = x.s.Religion,
                Ethnic = x.s.Ethnic,
                Point = x.s.Point,
            }).FirstOrDefaultAsync();

            return data;
        }

      
    }
}
