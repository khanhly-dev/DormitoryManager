using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Domain.AppEntites
{
    public class ContractTimeConfigEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSummerSemester { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
