﻿using System;
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
        public float StatBegin { get; set; }
        public float StatEnd { get; set; }
    }
}
