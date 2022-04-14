using Dormitory.Admin.Application.Catalog.StudentRepository.Dtos;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntites;
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
        Task<PageResult<StudentDto>> GetList(PageRequestBase request);
        Task<int> CreateOrUpdate(StudentEntity request);
        Task<int> Delete(int id);
        Task<int> UpdateContractFee(int contractId, DateTime datePaid, float moneyPaid);
        Task<PageResult<StudentDto>> GetAll(PageRequestBase request);
        Task<int> AddDiscipline(DisciplineEntity request);
        Task<int> UpdateDiscipline(DisciplineEntity request);
        Task<int> DeleteDiscipline(int disciplineId);
        Task<PageResult<DisciplineDto>> GetListDiscipline(PageRequestBase request);
        Task<List<ComboSelectDto>> GetListStudentSelect();
    }
}
