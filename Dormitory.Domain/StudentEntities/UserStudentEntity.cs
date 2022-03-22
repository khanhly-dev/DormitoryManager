using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Domain.StudentEntities
{
    public class UserStudentEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime DOB { get; set; }
        public string StudentCode { get; set; }
        public string Major { get; set; }
        public string Adress { get; set; }
        public string Class { get; set; }
    }
}
