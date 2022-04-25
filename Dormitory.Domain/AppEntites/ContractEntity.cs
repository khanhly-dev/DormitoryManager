using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Domain.AppEntities
{
    public class ContractEntity
    {
        public int Id { get; set; }
        public string ContractCode { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? SemesterId { get; set; }
        public int? RoomId { get; set; }
        public float? DesiredPrice { get; set; }
        public int StudentId { get; set; }
        public int? AdminConfirmStatus { get; set; }
        public int? StudentConfirmStatus { get; set; }
        public int? ContractCompletedStatus { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsExtendContract { get; set; }
        public bool IsSummerSemesterContract { get; set; }
    }
}
