using Dormitory.Student.Application.Catalog.SignUpDormitory.Dtos;
using Dormitory.Student.Application.Catalog.SignUpDormitory.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Student.Application.Catalog.SignUpDormitory
{
    public interface ISignUpDormitoryRepo
    {
        Task<int> SignUp(SignUpRequest request);
        Task<int> SetStudentPoint(SetStudentPointRepuest request);
        Task<List<CriteriaDto>> GetListCriteria();
        Task<int> StudentConfirmContract(int contractId, int confirmStatus);
        Task<int> Delete(int id);
        Task<int> CreateExtendContract(int studentId);
        Task<int> CreateSummerSemesterContract(int studentId);
        Task<ExtendContractTime> GetExtendContractTime(int studentId);
        Task<ExtendContractTime> GetSummerContractTime(int studentId);
    }
}
