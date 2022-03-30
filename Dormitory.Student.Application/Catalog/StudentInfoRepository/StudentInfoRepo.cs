using Dormitory.EntityFrameworkCore.AdminEntityFrameworkCore;
using Dormitory.Student.Application.Catalog.StudentInfoRepository.Dtos;
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
