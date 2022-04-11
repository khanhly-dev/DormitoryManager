using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.RoomRepository.Dtos
{
    public class RoomServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public int? FilledSlot { get; set; }
        public bool PaymentStatus { get; set; }
    }
}
