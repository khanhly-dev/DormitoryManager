using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Student.Application.Catalog.StudentInfoRepository.Dtos
{
    public class ContractPendingDto
    {
        public int Id { get; set; }
        public string ContractCode { get; set; }
        public DateTime DateCreated { get; set; }
        public float? DesiredPrice { get; set; }
        public int StudentId { get; set; }
        public int? RoomId { get; set; }
        public string RoomName { get; set; }
        public string AreaName { get; set; }
        public string StudentName { get; set; }
        public int Gender { get; set; }
        public string StudentPhone { get; set; }
        public string StudentCode { get; set; }
        public string Adress { get; set; }
        public int? Point { get; set; }
        public int? AdminConfirmStatus { get; set; }
        public int? StudentConfirmStatus { get; set; }
        public int? AcademicYear { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? FromDate { get; set; }
        public int? ContractCompletedStatus { get; set; }
        public bool? IsExtendContract { get; set; }
        public float? RoomPrice { get; set; }
        public bool IsDelete { get; set; }
        public bool IsSummerContract { get; set; }
    }
}
