using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Domain.AppEntites
{
    public class RoomServiceFeeEntity
    {
        public int Id { get; set; }
        public int RoomServiceId { get; set; }
        public float ServicePrice { get; set; }
        public DateTime? PaidDate { get; set; }
        public float? MoneyPaid { get; set; }
        public bool IsPaid { get; set; }
    }
}
