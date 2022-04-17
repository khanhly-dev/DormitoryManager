using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.RoomRepository.Dtos
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public int MaxSlot { get; set; }
        public int MinSlot { get; set; }
        public int EmptySlot { get; set; }
        public int? FilledSlot { get; set; }
        public int? AvaiableSlot { get; set; }
        public bool? IsPaid { get; set; }
        public float Dept { get; set; }
    }
}
