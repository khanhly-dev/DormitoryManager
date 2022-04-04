using Dormitory.Student.Application.CommonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Student.Application.Catalog.StudentInfoRepository.Dtos
{
    public class GetListContractByStudentIdRepuest : PageRequestBase
    {
        public int StudentId { get; set; }
    }
}
