using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Domain.AppEntities
{
    public class RoomServiceEntity
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int ServiceId { get; set; }
        public float Quantity { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public float ElecticStatBegin { get; set; }
        public float ElecticStatEnd { get; set; }
        public float WaterStatBegin { get; set; }
        public float WaterStatEnd { get; set; }
    }
}
