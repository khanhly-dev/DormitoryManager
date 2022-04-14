using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.StudentRepository.Dtos
{
    public class DisciplineDto
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Class { get; set; }
        public string StudentCode { get; set; }
        public string Phone { get; set; }
        public string Major { get; set; }
        public int Gender { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public string RoomName { get; set; }
        public string AreaName { get; set; }
        public string Punish { get; set; }
    }
}
