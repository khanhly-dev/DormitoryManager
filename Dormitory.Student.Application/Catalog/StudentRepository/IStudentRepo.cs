using Dormitory.Student.Application.Catalog.StudentRepository.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Student.Application.Catalog.StudentRepository
{
    public interface IStudentRepo
    {
        Task<List<StudentDto>> GetAllStudent();
    }
}
