using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Domain.AppEntities
{
    public class RoomEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int AreaId { get; set; }
        public int MaxSlot { get; set; }
        public int? MinSlot { get; set; }
        public int? EmptySlot { get; set; }
        public int? FilledSlot { get; set; }
        public int? AvaiableSlot { get; set; }
        public int? RoomGender { get; set; }
        public int? RoomAcedemic { get; set; }
    }
}
