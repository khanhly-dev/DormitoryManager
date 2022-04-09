using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Domain.AppEntites
{
    public class ContractFeeEntity
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public float ContractPriceValue { get; set; }
        public float RoomPrice { get; set; }
        public float ServicePrice { get; set; }
        public DateTime? PaidDate { get; set; }
        public float? MoneyPaid { get; set; }
        public bool IsPaid { get; set; }
    }
}
