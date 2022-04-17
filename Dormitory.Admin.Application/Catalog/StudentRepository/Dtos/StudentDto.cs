using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.StudentRepository.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string BaseAdress { get; set; }
        public string Adress { get; set; }
        public string Class { get; set; }
        public string StudentCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Major { get; set; }
        public int Gender { get; set; }
        public int? AcademicYear { get; set; }
        public string RelativeName { get; set; }
        public string RelativePhone { get; set; }
        public string Ethnic { get; set; }
        public string Religion { get; set; }
        public int? Point { get; set; }
        public bool PaymentStatus { get; set; }
        public int totalContract { get; set; }
        public float Dept { get; set; }
    }
}
