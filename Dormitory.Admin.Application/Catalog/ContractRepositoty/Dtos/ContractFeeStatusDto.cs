
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.ContractRepositoty.Dtos
{
    public class ContractFeeStatusDto
    {
        public int Id { get; set; }
        public string ContractCode { get; set; }
        public DateTime DateCreated { get; set; }
        public int StudentId { get; set; }
        public int? RoomId { get; set; }
        public string RoomName { get; set; }
        public float? RoomPrice { get; set; }
        public string AreaName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool? IsExtendContract { get; set; }
        public float ContractPriceValue { get; set; }
        public float ServicePrice { get; set; }
        public DateTime? PaidDate { get; set; }
        public float? MoneyPaid { get; set; }
        public bool IsPaid { get; set; }
        public float? ContractPrice { get; set; }
    }
}
