using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Student.Application.Catalog.SignUpDormitory.Requests
{
    public class SetStudentPointRepuest
    {
        public int StudentId { get; set; }
        public List<int> ListCriteriaId { get; set; }
    }
}
