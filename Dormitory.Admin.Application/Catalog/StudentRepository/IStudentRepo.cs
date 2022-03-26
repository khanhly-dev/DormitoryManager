using Dormitory.Admin.Application.Catalog.StudentRepository.Dtos;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.StudentRepository
{
    public interface IStudentRepo
    {
        public Task<PageResult<StudentDto>> GetList(PageRequestBase request);
        public Task<int> CreateOrUpdate(StudentEntity request);
        public Task<int> Delete(int id);
    }
}
