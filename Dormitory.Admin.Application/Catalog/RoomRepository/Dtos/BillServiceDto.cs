using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.RoomRepository.Dtos
{
    public class BillServiceDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int RoomId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public float TotalPrice { get; set; }
        public bool IsPaid { get; set; }
    }
}
