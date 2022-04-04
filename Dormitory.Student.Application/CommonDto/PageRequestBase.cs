using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Student.Application.CommonDto
{
    public class PageRequestBase : PageRequestResultBase
    {
        public string Keyword { get; set; }
    }
}
