using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.ContractRepositoty.Dtos
{
    public class ContractConfirmDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int Gender { get; set; }
        public int? AdminConfirmStatus { get; set; }
        public int? StudentConfirmStatus { get; set; }
        public int? ContractCompletedStatus { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool IsDelete { get; set; }
    }
}
