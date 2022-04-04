using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Student.Application.CommonDto
{
    public class PageRequestResultBase
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
