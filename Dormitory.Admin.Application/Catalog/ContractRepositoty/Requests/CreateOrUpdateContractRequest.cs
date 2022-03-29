using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.ContractRepositoty.Requests
{
    public class CreateOrUpdateContractRequest
    {
        public string ContractCode { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? RoomId { get; set; }
        public float? DesiredPrice { get; set; }
        public int StudentId { get; set; }
        public int? ServiceId { get; set; }
        public int? AdminConfirmStatus { get; set; }
        public int? StudentConfirmStatus { get; set; }
    }
}
