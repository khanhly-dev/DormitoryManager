using Dormitory.EntityFrameworkCore.StudentEntityFrameworkCore;
using Dormitory.Student.Application.Catalog.StudentRepository.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Student.Application.Catalog.StudentRepository
{
    public class StudentRepo : IStudentRepo
    {
        private readonly StudentSolutionDbContext _studentDbContext;
        public StudentRepo(StudentSolutionDbContext studentDbContext)
        {
            _studentDbContext = studentDbContext;
        }
        public async Task<List<StudentDto>> GetAllStudent()
        {
            var listStudent = await _studentDbContext.UserStudentEntities.AsNoTracking().Select(x => new StudentDto
            {
                Id = x.Id,
                Name = x.Name,
                DOB = x.DOB,
                StudentCode = x.StudentCode,
            }).ToListAsync();
            return listStudent;
        }
    }
}
