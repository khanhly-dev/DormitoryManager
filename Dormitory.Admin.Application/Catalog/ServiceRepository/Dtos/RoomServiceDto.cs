using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.ServiceRepository.Dtos
{
    public class RoomServiceDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public float Quantity { get; set; }
        public float StatBegin { get; set; }
        public float StatEnd { get; set; }
        public float TotalServicePrice { get; set; }
        public DateTime? PaidDate { get; set; }
        public float? MoneyPaid { get; set; }
        public bool IsPaid { get; set; }
    }
}
