using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.Dashboard.Dtos
{
    public class BaseStatDto
    {
        public int TotalSignUp { get; set; }
        public int TotalEmptySlot { get; set; }
        public float TotalContractDept { get; set; }
        public float TotalServiceDept { get; set; }
    }
}
