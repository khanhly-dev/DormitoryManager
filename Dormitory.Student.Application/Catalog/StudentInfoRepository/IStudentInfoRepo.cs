using Dormitory.Student.Application.Catalog.StudentInfoRepository.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Student.Application.Catalog.StudentInfoRepository
{
    public interface IStudentInfoRepo
    {
        Task<StudentInfoDto> GetStudentByUserId(int userId);
        Task<List<float>> GetRecomendRoomPrice();
    }
}
