using Dormitory.Domain.AppEntities;
using Dormitory.Student.Application.Catalog.StudentInfoRepository.Dtos;
using Dormitory.Student.Application.CommonDto;
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
        Task<PageResult<ContractPendingDto>> GetListStudentConfirmContractPending(GetListContractByStudentIdRepuest request);
        Task<bool> CheckCanSignUp(int studenId);
        Task<bool> CheckCanCreateExtendContract(int studentId);
        Task<bool> CheckCanCreateSummerContract(int studentId);
        Task<int> CreateStudent(StudentEntity student);
    }
}
