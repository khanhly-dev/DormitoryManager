using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.Dashboard.Dtos
{
    public class FeeDto
    {
        public int Id { get; set; }
        public string BillCode { get; set; }
        public float Fee { get; set; }
    }
}
